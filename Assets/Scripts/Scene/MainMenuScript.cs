using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button btnStart;
    public Button btnResume;
    private void Start()
    {
        // check if have a save file
        if (SaveLoad.LoadHeroesPosition().Count > 0)
        {
            btnResume.gameObject.SetActive(true);
            btnStart.gameObject.SetActive(false);
        }
        else
        {
            btnResume.gameObject.SetActive(false);
            btnStart.gameObject.SetActive(true);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
