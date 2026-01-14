using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;

    [Header("Sliders")]
    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider seSlider;

    [Header("Main Volume Buttons")]
    [SerializeField] private GameObject MuteMainButton;
    [SerializeField] private GameObject UnmuteMainButton;

    [Header("Music Volume Buttons")]
    [SerializeField] private GameObject MuteMusicButton;
    [SerializeField] private GameObject UnmuteMusicButton;

    [Header("SFX Volume Buttons")]
    [SerializeField] private GameObject MuteSEButton;
    [SerializeField] private GameObject UnmuteSEButton;

    private float lastMainVolume = 1f;
    private float lastMusicVolume = 1f;
    private float lastSEVolume = 1f;

    void Start()
    {
        mainSlider.onValueChanged.AddListener(SetMainVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        seSlider.onValueChanged.AddListener(SetSEVolume);

        SetMainVolume(mainSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSEVolume(seSlider.value);
    }

    public void SetMainVolume(float value)
    {
        lastMainVolume = value;
        mixer.SetFloat("MainVolume", Mathf.Log10(value / 100f) * 20);
        UnmuteMainButton.SetActive(false);
    }

    public void SetMusicVolume(float value)
    {
        lastMusicVolume = value;
        mixer.SetFloat("MusicVolume", Mathf.Log10(value / 100f) * 20);
        UnmuteMusicButton.SetActive(false);
    }

    public void SetSEVolume(float value)
    {
        lastSEVolume = value;
        mixer.SetFloat("SEVolume", Mathf.Log10(value / 100f) * 20);
        UnmuteSEButton.SetActive(false);
    }

    public void MuteMain()
    {
        mixer.SetFloat("MainVolume", -80f);
        UnmuteMainButton.SetActive(true);
    }

    public void UnmuteMain()
    {
        mixer.SetFloat("MainVolume", Mathf.Log10(lastMainVolume / 100f) * 20);
        UnmuteMainButton.SetActive(false);
    }

    public void MuteMusic()
    {
        mixer.SetFloat("MusicVolume", -80f);
        UnmuteMusicButton.SetActive(true);
    }

    public void UnmuteMusic()
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(lastMusicVolume / 100f) * 20);
        UnmuteMusicButton.SetActive(false);
    }

    public void MuteSE()
    {
        mixer.SetFloat("SEVolume", -80f);
        UnmuteSEButton.SetActive(true);
    }

    public void UnmuteSE()
    {
        mixer.SetFloat("SEVolume", Mathf.Log10(lastSEVolume / 100f) * 20);
        UnmuteSEButton.SetActive(false);
    }
}