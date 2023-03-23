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
            string json = JsonUtility.ToJson(new DataScore(currentScore));
            //save list to json file
            System.IO.File.WriteAllText(Application.dataPath + "/Scripts/Json/HighestScore.json", json);
        }
    }
    public static int LoadScore()
    {
        string path = Application.dataPath + "/Scripts/Json/HighestScore.json";
        // check if file doesn't exist, create it
        if (!System.IO.File.Exists(path))
        {
            string createJson = JsonUtility.ToJson(new DataScore(0));
            System.IO.File.WriteAllText(path, createJson);
        }
        string json = System.IO.File.ReadAllText(Application.dataPath + "/Scripts/Json/HighestScore.json");
        int highestScore = 0;
        if (json != null)
        {
            // convert json to ScoreData
            DataScore scoreData = JsonUtility.FromJson<DataScore>(json);
            highestScore = scoreData._highestScore;
        }

        return highestScore;
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
        string json = JsonUtility.ToJson(new DataSave(1, 1, 100, 0, 1000));
        //save list to json file
        System.IO.File.WriteAllText(Application.dataPath + "/Scripts/Json/SaveData.json", json);
    }
    public static void SaveData()
    {
        string json = JsonUtility.ToJson(new DataSave(CommonPropeties.currentRound, CommonPropeties.currentWave, CommonPropeties.healthOfVillage, CommonPropeties.currentScore, CommonPropeties.coin));
        //save list to json file
        System.IO.File.WriteAllText(Application.dataPath + "/Scripts/Json/SaveData.json", json);
    }
    public static void LoadData()
    {
        string path = Application.dataPath + "/Scripts/Json/SaveData.json";
        // check if file doesn't exist, create it
        if (!System.IO.File.Exists(path))
        {
            string createJson = JsonUtility.ToJson(new DataSave(1, 1, 100, 0, 1000));
            System.IO.File.WriteAllText(path, createJson);
        }
        string json = System.IO.File.ReadAllText(Application.dataPath + "/Scripts/Json/SaveData.json");
        if (json != null)
        {
            // convert json to ScoreData
            DataSave data = JsonUtility.FromJson<DataSave>(json);
            CommonPropeties.currentRound = data._currentRound;
            CommonPropeties.currentWave = data._currentWave;
            CommonPropeties.healthOfVillage = data._healthOfVillage;
            CommonPropeties.currentScore = data._currentScore;
            CommonPropeties.coin = data._coin;
        }
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
        DataHeroes emptyData = new DataHeroes(new List<Vector3>(), new List<bool>());
        string json = JsonUtility.ToJson(emptyData);
        System.IO.File.WriteAllText(Application.dataPath + "/Scripts/Json/SaveHeroes.json", json);
    }
    public static List<bool> LoadHeroesIsSleeping()
    {
        List<bool> heroesIsSleeping = new List<bool>();
        string path = Application.dataPath + "/Scripts/Json/SaveHeroes.json";
        // check if file doesn't exist, create it
        if (!System.IO.File.Exists(path))
        {
            string createJson = JsonUtility.ToJson(new DataHeroes(new List<Vector3>(), new List<bool>()));
            System.IO.File.WriteAllText(path, createJson);
        }
        string json = System.IO.File.ReadAllText(Application.dataPath + "/Scripts/Json/SaveHeroes.json");
        if (json != null)
        {
            // convert json to ScoreData
            DataHeroes dataHeroes = JsonUtility.FromJson<DataHeroes>(json);
            heroesIsSleeping = dataHeroes._listIsHeroSleeping;
        }
        return heroesIsSleeping;
    }
    public static List<Vector3> LoadHeroesPosition()
    {
        List<Vector3> heroesPosition = new List<Vector3>();
        string path = Application.dataPath + "/Scripts/Json/SaveHeroes.json";
        // check if file doesn't exist, create it
        if (!System.IO.File.Exists(path))
        {
            string createJson = JsonUtility.ToJson(new DataHeroes(new List<Vector3>(), new List<bool>()));
            System.IO.File.WriteAllText(path, createJson);
        }
        string json = System.IO.File.ReadAllText(Application.dataPath + "/Scripts/Json/SaveHeroes.json");
        if (json != null)
        {
            // convert json to ScoreData
            DataHeroes dataHeroes = JsonUtility.FromJson<DataHeroes>(json);
            heroesPosition = dataHeroes._listHeroes;
        }
        return heroesPosition;
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

        // Save to json file
        string json = JsonUtility.ToJson(new DataHeroes(allHeroesPositions, allIsSleeping));
        System.IO.File.WriteAllText(Application.dataPath + "/Scripts/Json/SaveHeroes.json", json);
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

