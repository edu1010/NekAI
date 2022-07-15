using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SliderMusicSync : MonoBehaviour
{
    public AudioMixer audioMixer;
    Slider slider;
    public void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = (MusicVolumen());

    }
    public float MusicVolumen()
    {
        float value;
        bool result = audioMixer.GetFloat("MusicVolume", out value);
        if (result)
        {
            return Mathf.Pow(10f, value / 20f);
        }
        else
        {
            return 0f;
        }
    }
   
}
