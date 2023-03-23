using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad
{
    public static void SaveScore(int currentScore)
    {
        int highestScore = LoadScore();
        if (currentScore >= highestScore)
        {
            string json = JsonUtility.ToJson(new ScoreData(currentScore));
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
            string createJson = JsonUtility.ToJson(new ScoreData(0));
            System.IO.File.WriteAllText(path, createJson);
        }
        string json = System.IO.File.ReadAllText(Application.dataPath + "/Scripts/Json/HighestScore.json");
        int highestScore = 0;
        if (json != null)
        {
            // convert json to ScoreData
            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);
            highestScore = scoreData._highestScore;
        }

        return highestScore;
    }

    [System.Serializable]
    public class ScoreData
    {
        public int _highestScore;

        public ScoreData(int highestScore)
        {
            _highestScore = highestScore;
        }
    }

}

