using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [SerializeField] private Animator optionsAnimator;
    [SerializeField] private GameObject resolutionDropdownGO;
    [SerializeField] private TMP_Dropdown graphicsDropdown;
    private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [SerializeField] private AudioMixer audioMixer;

    [Header("Volume sliders")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider fxSlider;
    [SerializeField] Slider ambientSlider;

    private Resolution[] resolutions;

    public void OpenOptions()
    {
        optionsAnimator.SetBool("IsOpen", true);
        FindCurrentSettings();
    }

    public void CloseOptions()
    {
        optionsAnimator.SetBool("IsOpen", false);
    }

    public void ApplyOptions()
    {
        SetGraphics(graphicsDropdown.value);
        SetFullscreen(fullscreenToggle.isOn);
        SetResolution();
    }

    private void SetResolution()
    {
        Resolution res = resolutions[resolutionDropdown.value];
        Screen.SetResolution(res.width, res.height, fullscreenToggle.isOn, res.refreshRate);
    }

    private void SetFullscreen(bool isFullscreen)
    {
        if (isFullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        Screen.fullScreen = isFullscreen;
    }

    private void SetGraphics(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    private void FindCurrentSettings()
    {
        resolutionDropdown = resolutionDropdownGO.GetComponent<TMP_Dropdown>();
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;

        foreach (Resolution res in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.ToString()));
        }


        int resolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionIndex = i;
            }
        }

        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen;
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();
    }

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("masterVolume", masterSlider.value);
    }
    public void SetMusicVolume()
    {
        audioMixer.SetFloat("musicVolume", musicSlider.value);
    }
    public void SetFXVolume()
    {
        audioMixer.SetFloat("fxVolume", fxSlider.value);
    }
    public void SetAmbientVolume()
    {
        audioMixer.SetFloat("ambientVolume", ambientSlider.value);
    }
}
