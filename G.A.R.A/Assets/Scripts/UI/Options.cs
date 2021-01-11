using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [SerializeField] private Animator optionsAnimator;
    [SerializeField] private Animator optionsAppliedAnim;
    [SerializeField] private GameObject resolutionDropdownGO;
    [SerializeField] private TMP_Dropdown graphicsDropdown;
    private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [SerializeField] private AudioMixer audioMixer;

    [HideInInspector] public bool isOpen;

    [Header("Volume sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider fxSlider;
    [SerializeField] private Slider ambientSlider;
    [SerializeField] private Slider uiSlider;

    [SerializeField] private TMP_Text testText;

    [SerializeField] private Canvas canvas;
    private CanvasRenderer canvasRend;

    private Resolution[] resolutions;

    private void Start()
    {
        canvasRend = canvas.GetComponent<CanvasRenderer>();
        if (audioMixer.GetFloat("masterVolume", out float masterVal))
        {
            masterSlider.value = masterVal;
        }
        if (audioMixer.GetFloat("musicVolume", out float musicVal))
        {
            musicSlider.value = musicVal;
        } 
        if(audioMixer.GetFloat("fxVolume", out float fxVal))
        {
            fxSlider.value = fxVal;
        }
        if(audioMixer.GetFloat("ambientVolume", out float ambientVal))
        {
            ambientSlider.value = ambientVal;
        }
        if(audioMixer.GetFloat("uiVolume", out float uiVal))
        {
            uiSlider.value = uiVal;
        }
        testText.gameObject.SetActive(false);
    }

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
        //optionsAppliedAnim.SetBool("Apply", true);
        optionsAppliedAnim.CrossFadeInFixedTime("ChangesApplied", .01f);
        SetGraphics(graphicsDropdown.value);
        SetFullscreen(fullscreenToggle.isOn);
        SetResolution();
    }

    private void SetOpened()
    {
        isOpen = true;
    }

    private void SetClosed()
    {
        isOpen = false;
    }

    private void SetResolution()
    {
        Resolution res = resolutions[resolutionDropdown.value];
        Screen.SetResolution(res.width, res.height, fullscreenToggle.isOn, res.refreshRate);

        testText.gameObject.SetActive(true); //This looks stupid, but if it works, it's not stupid :)
        testText.gameObject.SetActive(false); // don't remove please
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
    public void SetUIVolume()
    {
        audioMixer.SetFloat("uiVolume", uiSlider.value);
    }
}
