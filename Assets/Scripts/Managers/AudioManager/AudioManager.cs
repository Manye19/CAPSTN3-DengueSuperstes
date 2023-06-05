using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Components")]
    public Sound[] bgmSounds; 
    public Sound[] sfxSounds;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Awake()
    {
        SingletonManager.Register(this);

        // Move this to GameManager
        PlayBGM("Main BGM");
    }

    // play for the bgm music
    public void PlayBGM(string bgmName)
    {
        Sound s = Array.Find(bgmSounds, bgm => bgm.audioName == bgmName);

        if (s == null)
        {
            Debug.Log("Sound Not Found!");
            return;
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }

    }

    public void PlaySFX(string sfxName)
    {
        Sound s = Array.Find(sfxSounds, sfx => sfx.audioName == sfxName);

        if (s == null)
        {
            Debug.Log("Sound Not Found!");
            return;
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }

    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void AdjustMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void AdjustSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}
