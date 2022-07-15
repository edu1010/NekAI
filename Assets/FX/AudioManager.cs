using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioFile[] AudioFiles;
    public AudioFile[] MusicFiles => AudioFiles.Where(x => x.Type == AudioType.Music).ToArray();

    public string LastMusicFile { get => lastMusicFile; private set => lastMusicFile = value; }

    public AudioSource MusicSource;
    public List<AudioSource> SFXSourceList;
    private int sourceIndex;
    public AudioSource JetpackSFXSource;

    public AudioMixer AudioMixer;
    private string lastMusicFile;
    [Range(0, 1)]
    public float GlobalMusicVolume = 1;
    [Range(0, 1)]
    public float GlobalSFXVolume = 1;

    private void Awake()
    {
        sourceIndex = 0;

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
    public static void PlayMusic(string name)
    {
        Instance.LastMusicFile = name;
        Instance._PlayMusic(name);
   }
    public static void PlayRandomMusic()
    {
        Instance._PlayRandomMusic();
    }
    public static void PlaySFX(string name)
    {
        Instance._PlaySFX(name);
    }

    public static void StopSFX(){
        Instance._StopJetpackSFX();
    }
    public static void PlaySFX(string name, AudioSource audioSource)
    {
        if (!GameFlowController.Instance.debug)
        {
            Instance._PlaySFX(name, audioSource);
        }
     }
   


    private void _PlayMusic(string name)
    {
        var clip = GetFileByName(name);
        if (clip != null)
        {
            float vol = GlobalMusicVolume * clip.Volume;
            MusicSource.volume = vol;
            MusicSource.clip = clip.Clip;
            MusicSource.Play();
        }
        else
        {
            Debug.LogError(" No such audio file " + name);
        }
    }
    private void _PlayRandomMusic()
    {
        var musics = MusicFiles;
        int rdm = Random.Range(0, musics.Length);
        _PlayMusic(musics[rdm].Name);
    }
    private void _PlaySFX(string name)
    {

        var clip = GetFileByName(name);
        if (clip != null)
        {
            float vol = GlobalSFXVolume * clip.Volume;

            if(name.Equals("jetpack")){
                JetpackSFXSource.volume = vol;
                JetpackSFXSource.clip = clip.Clip;
                JetpackSFXSource.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(name).Length > 0 ?
                   AudioMixer.FindMatchingGroups(name)[0] :
                   AudioMixer.FindMatchingGroups("Sfx")[0];

                JetpackSFXSource.Play();
            }else{

                if(sourceIndex >= SFXSourceList.Count()) sourceIndex = 0;

                AudioSource audioSource = SFXSourceList.ElementAt(sourceIndex);

                audioSource.volume = vol;
                audioSource.clip = clip.Clip;
                audioSource.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(name).Length > 0 ?
                   AudioMixer.FindMatchingGroups(name)[0] :
                   AudioMixer.FindMatchingGroups("Sfx")[0];

                audioSource.Play();

                

                sourceIndex++;
            }    
        }
        else
        {
            Debug.LogError(" No such audio file " + name);
        }
    }

    private void _StopJetpackSFX(){
        JetpackSFXSource.Stop();
    }
    private void _PlaySFX(string name, AudioSource audioSource)
    {
        var clip = GetFileByName(name);
        if (clip != null)
        {
            float vol = GlobalSFXVolume * clip.Volume;
            audioSource.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(name).Length > 0 ?
                    AudioMixer.FindMatchingGroups(name)[0] :
                    AudioMixer.FindMatchingGroups("Sfx")[0];
            audioSource.volume = vol;
            audioSource.clip = clip.Clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError(" No such audio file " + name);
        }
    }
    private void _PlaySFXLaser(string name, AudioSource audioSource)
    {
        var clip = GetFileByName(name);
        if (clip != null)
        {
            float vol = GlobalSFXVolume * clip.Volume;
            audioSource.outputAudioMixerGroup = AudioMixer.FindMatchingGroups(name).Length > 0 ?
                    AudioMixer.FindMatchingGroups(name)[0] :
                    AudioMixer.FindMatchingGroups("Sfx")[0];
            audioSource.volume = vol;
            audioSource.clip = clip.Clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError(" No such audio file " + name);
        }
    }


    private AudioFile GetFileByName(string name)
    {
        return AudioFiles.First(x => x.Name == name);
    }   
    
}



[Serializable]
public class AudioFile
{
    public string Name;
    public AudioClip Clip => Clips[Random.Range(0, Clips.Length)];
    public AudioClip[] Clips;
    [Range(0, 1)]
    public float Volume;
    public AudioType Type;




}

public enum AudioType
{
    Music,
    Sfx,
    Laser,
    Envirmonment
}