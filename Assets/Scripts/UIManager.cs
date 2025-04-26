using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private SoundManager soundManager;
    
    public Slider musicSlider, sfxSlider, generalSlider;

    public GameObject optionsPanel, creditsPanel;

    void Start(){
    }
    public void SetMusicSliderValue(float value)
    {
        musicSlider.value = value;
    }
    public void SetSFXSliderValue(float value)
    {
        sfxSlider.value = value;
    }
    public void SetGeneralSliderValue(float value)
    {
        generalSlider.value = value;
    }
    public void UpdateSliderValues()
    {
        SetMusicSliderValue(soundManager.GetMusicVolume());
        SetSFXSliderValue(soundManager.GetSFXVolume());
        SetGeneralSliderValue(soundManager.GetMasterVolume());
    }

    public void OpenOptionsPanel()
    {
        optionsPanel = FindObjectOfType<OptionsPanel>(true).gameObject;
        InitializeSliders();
        soundManager = FindObjectOfType<SoundManager>();
        UpdateSliderValues();
        optionsPanel.SetActive(true);
        optionsPanel.transform.DOLocalMoveX(0, 1f).SetEase(Ease.OutBounce);

    }
    public void CloseOptionsPanel()
    {
        optionsPanel = FindObjectOfType<OptionsPanel>(true).gameObject;
        if (Time.timeScale != 1)
        {
            optionsPanel.SetActive(false);
        }
        else
        {
            optionsPanel.transform.DOLocalMoveX(2500, 2f).SetEase(Ease.OutBounce);
        }

    }

    public void SetOptionsPanel(GameObject optionsPanel)
    {
        this.optionsPanel = optionsPanel;
    }

    // Get the sliders references from the optionsPanel
    public void InitializeSliders()
    {
        Slider[] sliders = optionsPanel.GetComponentsInChildren<Slider>();
        generalSlider = sliders[0];
        musicSlider = sliders[1];
        sfxSlider = sliders[2];
    }

    public void OpenCreditsPanel()
    {
        creditsPanel = FindObjectOfType<Credits>(true).gameObject;
        creditsPanel.SetActive(true);
        // Slowly tweens the Pos Y of the credits panel to 0
        creditsPanel.transform.DOLocalMoveY(0, 2f).SetEase(Ease.OutBounce);
        
    }
    
    public void CloseCreditsPanel()
    {
        creditsPanel = FindObjectOfType<Credits>(true).gameObject;
        creditsPanel.SetActive(false);
    }
    
    //Wait for 3 seconds then open credits panel
    public IEnumerator OpenCreditsPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OpenCreditsPanel();
    }
}
