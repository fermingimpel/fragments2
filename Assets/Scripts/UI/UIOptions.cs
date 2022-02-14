using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptions : MonoBehaviour {

    [SerializeField] Slider horizontalSensitivitySlider;
    [SerializeField] Slider verticalSensitivitySlider;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;

    [SerializeField] Text horSensitivityText;
    [SerializeField] Text verSensitivityText;
    [SerializeField] Text musicVolumeText;
    [SerializeField] Text sfxVolumeText;

    void Start() {
        horizontalSensitivitySlider.value = GameInstance.instance.HorizontalSensitivity;
        verticalSensitivitySlider.value = GameInstance.instance.VerticalSensitivity;
        sfxVolumeSlider.value = GameInstance.instance.SfxVolume;
        musicVolumeSlider.value = GameInstance.instance.MusicVolume;

        horSensitivityText.text = ((int)GameInstance.instance.HorizontalSensitivity).ToString();
        horSensitivityText.text = ((int)GameInstance.instance.VerticalSensitivity).ToString();
        sfxVolumeText.text = ((int)GameInstance.instance.SfxVolume).ToString();
        musicVolumeText.text = ((int)GameInstance.instance.MusicVolume).ToString();
    }

    public void UpdateHorizontalSensitivity() {
        GameInstance.instance.HorizontalSensitivity = horizontalSensitivitySlider.value;
        horSensitivityText.text = ((int)horizontalSensitivitySlider.value).ToString();
    }

    public void UpdateVerticalSensitivity() {
        GameInstance.instance.VerticalSensitivity = verticalSensitivitySlider.value;
        verSensitivityText.text = ((int)verticalSensitivitySlider.value).ToString();
    }

    public void UpdateSFXVolume() {
        GameInstance.instance.SfxVolume = sfxVolumeSlider.value;
        sfxVolumeText.text = sfxVolumeSlider.value.ToString("F1");
    }

    public void UpdateMusicVolume() {
        GameInstance.instance.MusicVolume = musicVolumeSlider.value;
        musicVolumeText.text = musicVolumeSlider.value.ToString("F1");
    }

}
