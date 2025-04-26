using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private  AudioClip[] audioClips;
    [SerializeField]
    private  AudioSource sfxAudioSource, musicAudioSource;
    public AudioMixer audioMixer;

    private UIManager uiManager;

    public event Action<float> MusicVolumeInitialized;

    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    void Start()
    {

        uiManager = GetComponent<UIManager>();
        InitializePlayerSoundPrefs();
        musicAudioSource.clip = audioClips[0];
        musicAudioSource.Play();

    }


    public void SetMasterVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        masterVolume = volume;
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        musicVolume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        MusicVolumeInitialized?.Invoke(volume);
    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        audioMixer.SetFloat("SfxVolume", Mathf.Log10(volume) * 20);
        sfxVolume = volume;
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
    }

    public void InitializePlayerSoundPrefs()
    {
        SetMasterVolume(PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f));
        SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f));
        uiManager.SetGeneralSliderValue(masterVolume);
        uiManager.SetMusicSliderValue(musicVolume);
        uiManager.SetSFXSliderValue(sfxVolume);
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }
    public float GetMusicVolume()
    {
        return musicVolume;
    }
    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public void PlaySFX(int index)
    {
        sfxAudioSource.PlayOneShot(audioClips[index]);
    }

    public void PlayMusic(int index)
    {
        musicAudioSource.clip = audioClips[index];
        musicAudioSource.Play();
    }

}
