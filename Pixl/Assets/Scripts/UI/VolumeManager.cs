using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private const string exposedParam = "MusicVolume";

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetVolume);

        SetVolume(volumeSlider.value);
    }

    public void SetVolume(float value)
    {
        float normalized = Mathf.Clamp(value / 100f, 0.0001f, 1f);

        float dB = Mathf.Log10(normalized) * 20f;

        audioMixer.SetFloat(exposedParam, dB);
    }

    public void ToggleMute()
    {
        audioMixer.SetFloat(exposedParam, -80f);
    }

    public void Unmute()
    {
        float normalized = Mathf.Clamp(volumeSlider.value / 100f, 0.0001f, 1f);
        float dB = Mathf.Log10(normalized) * 20f;

        audioMixer.SetFloat(exposedParam, dB);
    }
}