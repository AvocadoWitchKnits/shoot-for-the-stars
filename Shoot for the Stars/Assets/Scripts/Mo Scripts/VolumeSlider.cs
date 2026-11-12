using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour

{
    [SerializeField] private Slider slider;

    void Start()
    {
        // Load the saved volume (defaults to 1 = full volume)
        slider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        AudioListener.volume = slider.value;

        slider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
}

