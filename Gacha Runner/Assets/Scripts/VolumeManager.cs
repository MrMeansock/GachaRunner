using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    private float masterVolume;
    public float MasterVolume
    {
        get => masterVolume;
        set
        {
            masterVolume = value;
            AkSoundEngine.SetRTPCValue("MasterVolume", masterVolume);
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        }
    }

    public bool MasterIsMuted { get; private set; }
    public void SetMasterMute(bool mute)
    {
        MasterIsMuted = mute;
        PlayerPrefs.SetInt("MasterMute", MasterIsMuted ? 1 : 0);

        if (mute)
        {
            AkSoundEngine.SetRTPCValue("MasterVolume", 0);
        }
        else
        {
            AkSoundEngine.SetRTPCValue("MasterVolume", masterVolume);
        }
    }

    private float musicVolume;
    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = value;
            AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }
    }

    public bool MusicIsMuted { get; private set; }
    public void SetMusicMute(bool mute)
    {
        MusicIsMuted = mute;
        PlayerPrefs.SetInt("MusicMute", MusicIsMuted ? 1 : 0);

        if (mute)
        {
            AkSoundEngine.SetRTPCValue("MusicVolume", 0);
        }
        else
        {
            AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
        }
    }

    private float sfxVolume;
    public float SFXVolume
    {
        get => sfxVolume;
        set
        {
            sfxVolume = value;
            AkSoundEngine.SetRTPCValue("SFXVolume", sfxVolume);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        }
    }

    public bool SFXIsMuted { get; private set; }
    public void SetSFXMute(bool mute)
    {
        SFXIsMuted = mute;
        PlayerPrefs.SetInt("SFXMute", SFXIsMuted ? 1 : 0);

        if (mute)
        {
            AkSoundEngine.SetRTPCValue("SFXVolume", 0);
        }
        else
        {
            AkSoundEngine.SetRTPCValue("SFXVolume", sfxVolume);
        }
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MasterVolume")) MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
        else MasterVolume = 75.0f;
        if (PlayerPrefs.HasKey("MusicVolume")) MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        else MusicVolume = 75.0f;
        if (PlayerPrefs.HasKey("SFXVolume")) SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        else SFXVolume = 75.0f;

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
}
