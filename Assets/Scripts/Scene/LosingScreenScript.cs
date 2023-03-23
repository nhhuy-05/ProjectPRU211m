using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosingScreenScript : MonoBehaviour
{
    public TextMeshProUGUI Notification;
    // Start is called before the first frame update
    void Start()
    {
        int highestScore = SaveLoad.LoadScore();
        // check if have a new high score
        if (CommonPropeties.score >= highestScore)
        {
            Notification.text = "Game Over!\nNew highest score: " + CommonPropeties.score;
        }
        else
        {
            Notification.text = "Game Over!\nYour Score: " + CommonPropeties.score;
        }
        Invoke("loadMainMenu", 5f);
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

