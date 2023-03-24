using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoad
{
    // Save and Load Score
    public static void SaveScore(int currentScore)
    {
        int highestScore = LoadScore();
        if (currentScore >= highestScore)
        {
            PlayerPrefs.SetInt("HighestScore", currentScore);
        }
    }
    public static int LoadScore()
    {
        return PlayerPrefs.GetInt("HighestScore");
    }

    [System.Serializable]
    public class DataScore
    {
        public int _highestScore;

        public DataScore(int highestScore)
        {
            _highestScore = highestScore;
        }
    }

    // Save and Load Common Data
    public static void ClearDataSaveFile()
    {
        DataSave data = new DataSave(1, 1, 100, 0, 500);
        PlayerPrefs.SetInt("CurrentRound", data._currentRound);
        PlayerPrefs.SetInt("CurrentWave", data._currentWave);
        PlayerPrefs.SetInt("HealthOfVillage", data._healthOfVillage);
        PlayerPrefs.SetInt("CurrentScore", data._currentScore);
        PlayerPrefs.SetInt("Coin", data._coin);
    }
    public static void SaveData()
    {
        DataSave data = new DataSave(CommonPropeties.currentRound, CommonPropeties.currentWave, CommonPropeties.healthOfVillage, CommonPropeties.currentScore, CommonPropeties.coin);
        PlayerPrefs.SetInt("CurrentRound", data._currentRound);
        PlayerPrefs.SetInt("CurrentWave", data._currentWave);
        PlayerPrefs.SetInt("HealthOfVillage", data._healthOfVillage);
        PlayerPrefs.SetInt("CurrentScore", data._currentScore);
        PlayerPrefs.SetInt("Coin", data._coin);
    }
    public static void LoadData()
    {
        if (PlayerPrefs.GetInt("CurrentRound")==0)
        {
            ClearDataSaveFile();
        }
        CommonPropeties.currentRound = PlayerPrefs.GetInt("CurrentRound");
        CommonPropeties.currentWave = PlayerPrefs.GetInt("CurrentWave");
        CommonPropeties.healthOfVillage = PlayerPrefs.GetInt("HealthOfVillage");
        CommonPropeties.currentScore = PlayerPrefs.GetInt("CurrentScore");
        CommonPropeties.coin = PlayerPrefs.GetInt("Coin");
    }

    [System.Serializable]
    public class DataSave
    {
        public int _currentRound;
        public int _currentWave;
        public int _healthOfVillage;
        public int _currentScore;
        public int _coin;

        public DataSave(int currentRound, int currentWave, int healthOfVillage, int currentScore, int coin)
        {
            _currentWave = currentWave;
            _currentRound = currentRound;
            _healthOfVillage = healthOfVillage;
            _currentScore = currentScore;
            _coin = coin;
        }
    }

    // Save And Load Heroes
    public static void ClearHeroesSaveFile()
    {
        string json = JsonUtility.ToJson(new DataHeroes(new List<Vector3>(), new List<bool>()));
        PlayerPrefs.SetString("HeroesData", json);
    }
    public static List<bool> LoadHeroesIsSleeping()
    {
        string json = PlayerPrefs.GetString("HeroesData");
        if (json == "")
        {
            ClearHeroesSaveFile();
        }
        DataHeroes data = JsonUtility.FromJson<DataHeroes>(json);
        return data._listIsHeroSleeping;
    }
    public static List<Vector3> LoadHeroesPosition()
    {
        List<Vector3> list = new List<Vector3>();
        string json = PlayerPrefs.GetString("HeroesData");
        if (json == "")
        {
            ClearHeroesSaveFile();
        }
        DataHeroes data = JsonUtility.FromJson<DataHeroes>(json);
        return data._listHeroes;
    }
    public static void SaveHeroes()
    {
        // Get all heroes in the scene
        List<GameObject> listCowboys = GameObject.FindGameObjectsWithTag("BodyCowboy").ToList();
        List<Vector3> listCowboyVector3 = GetListVector3(listCowboys);
        List<bool> listCowboyIsSleeping = GetListIsSleeping(listCowboys);

        List<GameObject> listArchers = GameObject.FindGameObjectsWithTag("BodyArcher").ToList();
        List<Vector3> listArcherVector3 = GetListVector3(listArchers);
        List<bool> listArcherIsSleeping = GetListIsSleeping(listArchers);

        List<GameObject> listTanks = GameObject.FindGameObjectsWithTag("BodyTank").ToList();
        List<Vector3> listTankVector3 = GetListVector3(listTanks);
        List<bool> listTankIsSleeping = GetListIsSleeping(listTanks);

        List<GameObject> listWizard = GameObject.FindGameObjectsWithTag("BodyWizard").ToList();
        List<Vector3> listWizardVector3 = GetListVector3(listWizard);
        List<bool> listWizardIsSleeping = GetListIsSleeping(listWizard);

        List<Vector3> allHeroesPositions = new List<Vector3>();
        allHeroesPositions.AddRange(listCowboyVector3);
        allHeroesPositions.AddRange(listArcherVector3);
        allHeroesPositions.AddRange(listTankVector3);
        allHeroesPositions.AddRange(listWizardVector3);

        List<bool> allIsSleeping = new List<bool>();
        allIsSleeping.AddRange(listCowboyIsSleeping);
        allIsSleeping.AddRange(listArcherIsSleeping);
        allIsSleeping.AddRange(listTankIsSleeping);
        allIsSleeping.AddRange(listWizardIsSleeping);

        DataHeroes data = new DataHeroes(allHeroesPositions, allIsSleeping);
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("HeroesData", json);
    }
    public static List<Vector3> GetListVector3(List<GameObject> lists)
    {
        List<Vector3> listVector3 = new List<Vector3>();
        foreach (GameObject item in lists)
        {
            listVector3.Add(item.transform.position);
        }
        return listVector3;
    }
    public static List<bool> GetListIsSleeping(List<GameObject> lists)
    {
        List<bool> listIsSleeping = new List<bool>();
        foreach (GameObject item in lists)
        {
            if (item.transform.Find("SleepingEffect").GetComponent<ParticleSystem>().isPlaying)
            {
                listIsSleeping.Add(true);
            }
            else
            {
                listIsSleeping.Add(false);
            }
        }
        return listIsSleeping;

    }
    [System.Serializable]
    public class DataHeroes
    {
        public List<Vector3> _listHeroes;
        public List<bool> _listIsHeroSleeping;

        public DataHeroes(List<Vector3> listHeroes, List<bool> listIsHeroSleeping)
        {
            _listHeroes = listHeroes;
            _listIsHeroSleeping = listIsHeroSleeping;
        }
    }
}

