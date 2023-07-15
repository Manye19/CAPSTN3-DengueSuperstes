using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UiController : MonoBehaviour
{
    // will reimplement the stuff here to the UiManager
    // move this and place in one region

    //private UnityEvent 

    private AudioManager audioMgr;

    // [Header("Pause Screen Sliders")]
    // public Slider pauseMusicSlider;
    // public Slider pauseSfxSlider;

    [Header ("Settings Screen Sliders")]
    public Slider settingsMusicSlider;
    public Slider settingsSfxSlider;

    void Start()
    {
        audioMgr = SingletonManager.Get<AudioManager>();

        // initialize the values of the sliders to 0.7
        audioMgr.AdjustMusicVolume(0.7f);
        audioMgr.AdjustSFXVolume(0.7f);

    }

    public void ToggleMusic()
    {
        audioMgr.ToggleMusic();
    }

    public void ToggleSFX()
    {
        audioMgr.ToggleSFX();
    }

    // Sliders Adjusting
    public void AdjustMusicVolume()
    {
        //audioMgr.AdjustMusicVolume(pauseMusicSlider.value);
        audioMgr.AdjustMusicVolume(settingsMusicSlider.value);
    }

    public void AdjustSFXVolume()
    {
        //audioMgr.AdjustSFXVolume(pauseSfxSlider.value);
        audioMgr.AdjustSFXVolume(settingsSfxSlider.value);
    }

    public void OnButtonClick()
    {
        audioMgr.PlaySFX("Button Click");
    }

    public void OnSFXSliderReleased()
    {
        // insert a good sfx for this part
        audioMgr.PlaySFX("Button Click 3");
    }

    public void OnBackButtonClicked()
    {
        audioMgr.PlaySFX("Button Click");
    }

    public void OnEnterButtonClicked()
    {
        audioMgr.PlaySFX("Button Click 2");
    }

    public void OnEnterPlayButtonClicked()
    {
        audioMgr.PlaySFX("Button Click 3");
    }

    
}
