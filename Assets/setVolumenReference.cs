using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setVolumenReference : MonoBehaviour
{

    public void audioMixerMusic(float volume)
    {
        SetVolume.Instance.audioMixerMusic(volume);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
