using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Slider masterVolumeSlider;

    public AudioMixer audioMixer;

    public float sensitivity = 0.15f;
    public float masterVolume = 0.15f;
    // public TMP_Dropdown resolutionDropdown;
    // public Toggle fullscreenToggle;

    // private Resolution[] resolutions;


    void Start()
    {
        sensitivitySlider.value = sensitivity;
        masterVolumeSlider.value = masterVolume;
    }
    public void SetSensitivity(float value)
    {
        sensitivity = value;
    }
    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        float volumeInDecibels = Mathf.Log10(value) * 20f;
        audioMixer.SetFloat("MasterVolume", volumeInDecibels);
    }
}
