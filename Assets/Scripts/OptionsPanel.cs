using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    private UIManager uiManager;
    private SoundManager soundManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    public void CloseOptionsPanel()
    {
        uiManager = FindObjectOfType<UIManager>();
        uiManager.CloseOptionsPanel();
    }
    public void OpenOptionsPanel()
    {
        uiManager = FindObjectOfType<UIManager>();
        uiManager.OpenOptionsPanel();
        soundManager = FindObjectOfType<SoundManager>();
    }
    public void SetMasterVolume(float volume)
    {
        soundManager = FindObjectOfType<SoundManager>();

        soundManager.SetMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
       soundManager = FindObjectOfType<SoundManager>();
       soundManager.SetSFXVolume(volume);
    }
}
