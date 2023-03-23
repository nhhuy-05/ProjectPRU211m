using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float Volume)
    {
        audioMixer.SetFloat("volume", Volume);
    }
}
