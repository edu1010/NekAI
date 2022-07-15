using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : ExtendedMonoBehaviour
{
    public AudioMixer audioMixer;

    public static SetVolume Instance { get; private set; }
    float lastVolume = 1;
    float lastVolumeSfx = 1;
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
            InvokeLater(() => audioMixerMusic(lastVolume), 0.5f);
            InvokeLater(() => SFXMixerMusic(lastVolumeSfx), 0.5f);
            
            
            
        }
    }
    public void audioMixerMusic(float volume)//Modifico el master
    {
        
        if (volume == 0)
        {
            volume = 0.001f;
        }
        lastVolume = volume;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        //audioMixer.SetFloat("MasterVolume", volume);
    }
    public void SFXMixerMusic(float volume)
    {
        if (volume == 0)
        {
            volume = 0.001f;
        }
        lastVolumeSfx = volume;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}
