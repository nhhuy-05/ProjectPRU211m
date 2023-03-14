using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    //public TMP_Text popUpText;

    public void PopUp( )
    {
        popUpBox.SetActive(true);
        //popUpText.text = text;
    }
}
