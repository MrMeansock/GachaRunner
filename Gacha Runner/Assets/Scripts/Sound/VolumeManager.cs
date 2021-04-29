using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    // Dependencies
    private AudioMixer mainMixer = null;

    private float masterVolume;
    public float MasterVolume
    {
        get => masterVolume;
        set
        {
            masterVolume = value;
            mainMixer.SetFloat("MasterVolume", PercentToMixerDB(masterVolume));
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        }
    }

    public bool MasterIsMuted { get; private set; }
    public void SetMasterMute(bool mute)
    {
        MasterIsMuted = mute;
        mainMixer.SetFloat("MasterVolume", MasterIsMuted ? PercentToMixerDB(0) : PercentToMixerDB(masterVolume));
        PlayerPrefs.SetInt("MasterMute", MasterIsMuted ? 1 : 0);
    }

    private float musicVolume;
    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = value;
            mainMixer.SetFloat("MusicVolume", PercentToMixerDB(musicVolume));
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }
    }

    public bool MusicIsMuted { get; private set; }
    public void SetMusicMute(bool mute)
    {
        MusicIsMuted = mute;
        mainMixer.SetFloat("MusicVolume", MusicIsMuted ? PercentToMixerDB(0) : PercentToMixerDB(musicVolume));
        PlayerPrefs.SetInt("MusicMute", MusicIsMuted ? 1 : 0);
    }

    private float sfxVolume;
    public float SFXVolume
    {
        get => sfxVolume;
        set
        {
            sfxVolume = value;
            mainMixer.SetFloat("SFXVolume", PercentToMixerDB(sfxVolume));
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        }
    }

    public bool SFXIsMuted { get; private set; }
    public void SetSFXMute(bool mute)
    {
        SFXIsMuted = mute;
        mainMixer.SetFloat("SFXVolume", SFXIsMuted ? PercentToMixerDB(0) : PercentToMixerDB(sfxVolume));
        PlayerPrefs.SetInt("SFXMute", SFXIsMuted ? 1 : 0);
    }

    private void Awake()
    {
        mainMixer = GetComponentInChildren<Mixer>().mixer;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume")) MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        else MasterVolume = 0.75f;
        if (PlayerPrefs.HasKey("MusicVolume")) MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        else MusicVolume = 0.75f;
        if (PlayerPrefs.HasKey("SFXVolume")) SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        else SFXVolume = 0.75f;

        int masterMute = PlayerPrefs.GetInt("MasterMute");
        if (masterMute == 0)
        {
            SetMasterMute(false);
        }
        else
        {
            SetMasterMute(true);
        }
        int musicMute = PlayerPrefs.GetInt("MusicMute");
        if (musicMute == 0)
        {
            SetMusicMute(false);
        }
        else
        {
            SetMusicMute(true);
        }
        int sfxMute = PlayerPrefs.GetInt("SFXMute");
        if (sfxMute == 0)
        {
            SetSFXMute(false);
        }
        else
        {
            SetSFXMute(true);
        }
    }

    // Helper 0-1 to Mixer DB's conversion function
    private float PercentToMixerDB(float percent)
    {
        return Mathf.Lerp(-80f, 5, percent);
    }
}