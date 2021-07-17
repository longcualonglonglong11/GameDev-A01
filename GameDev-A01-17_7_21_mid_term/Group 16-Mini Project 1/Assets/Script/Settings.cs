using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        InitResolution(resolutions);
        InitVolume();
    }

    void InitResolution(Resolution[] resolutions)
    {
        if (resolutionDropdown == null) return;
        if (!PlayerPrefs.HasKey("ResolutionHeight")){
            PlayerPrefs.SetInt("ResolutionHeight", Screen.currentResolution.height);
            PlayerPrefs.SetInt("ResolutionWidth", Screen.currentResolution.width);
        }
        LoadResolutionSetting();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            int width = resolutions[i].width;
            int height = resolutions[i].height;
            string option = width + " x " + height;
            options.Add(option);
            if (width == Screen.currentResolution.width &&
                height == Screen.currentResolution.height)
            {
                currResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void InitVolume()
    {
        if (volumeSlider == null) return;
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        LoadVolumeSetting();
    }
    public void ChangeResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        SaveResolutionSetting(resolution.width, resolution.height);
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolumeSetting();
    }
    private void LoadVolumeSetting()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void SaveVolumeSetting()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    private void LoadResolutionSetting()
    {
        int width = PlayerPrefs.GetInt("ResolutionWidth");
        int height = PlayerPrefs.GetInt("ResolutionHeight");
        Screen.SetResolution(width, height, Screen.fullScreen);
        Debug.Log("LoadReshieght " + Screen.currentResolution.height+ " " + height);
        Debug.Log("LoadReswidth " + Screen.currentResolution.width + " " + width);
    }
    private void SaveResolutionSetting(int width, int height)
    {
        PlayerPrefs.SetInt("ResolutionHeight", height);
        PlayerPrefs.SetInt("ResolutionWidth", width);
        Debug.Log("SaveReshieght " + PlayerPrefs.GetInt("ResolutionHeight"));
        Debug.Log("SaveReswidth " + PlayerPrefs.GetInt("ResolutionWidth"));
    }
}
