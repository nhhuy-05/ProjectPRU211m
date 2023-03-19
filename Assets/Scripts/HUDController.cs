using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public HealthBarContainer _HealthVilage;
    public TextMeshProUGUI _coin;
    private void Start()
    {
        _HealthVilage.SetMaxHealth(CommonPropeties.healthOfVillage);
        _coin.text = CommonPropeties.coin.ToString();
    }
    private void Update()
    {
        _coin.text = CommonPropeties.coin.ToString();
        _HealthVilage.SetHealth(CommonPropeties.healthOfVillage);
    }
}
