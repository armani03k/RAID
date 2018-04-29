using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {


    public Slider VolumeSlider;
    public Dropdown GraphicsDropDown;
    public AudioMixer MainAudio;

    public void Play(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SetQualityValue()
    {
        GraphicsDropDown.value = QualitySettings.GetQualityLevel();
    }

    public void SetSliderValue()
    {
        float tempValue;
        MainAudio.GetFloat("volume", out tempValue);
        VolumeSlider.value = tempValue;
    }

    public void SetVolume(float volume)
    {
        MainAudio.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
