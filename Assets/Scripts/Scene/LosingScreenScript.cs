using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosingScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("loadMainMenu", 2f);
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

