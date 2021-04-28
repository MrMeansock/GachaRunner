using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class VolumeManager : MonoBehaviour
{
    // Dependencies
    private AudioSource sfxSource = null;
    private AudioSource musicSource = null;

    private float masterVolume;
    public float MasterVolume
    {
        get => masterVolume;
        set
        {
            masterVolume = value;

            sfxSource.volume = sfxVolume * masterVolume;
            musicSource.volume = musicVolume * masterVolume;

            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        }
    }

    public bool MasterIsMuted { get; private set; }
    public void SetMasterMute(bool mute)
    {
        MasterIsMuted = mute;
        PlayerPrefs.SetInt("MasterMute", MasterIsMuted ? 1 : 0);

        sfxSource.mute = MasterIsMuted;
        musicSource.mute = MasterIsMuted;
    }

    private float musicVolume;
    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            musicVolume = value;

            musicSource.volume = musicVolume;
            
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }
    }

    public bool MusicIsMuted { get; private set; }
    public void SetMusicMute(bool mute)
    {
        MusicIsMuted = mute;
        PlayerPrefs.SetInt("MusicMute", MusicIsMuted ? 1 : 0);

        musicSource.mute = MusicIsMuted;
    }

    private float sfxVolume;
    public float SFXVolume
    {
        get => sfxVolume;
        set
        {
            sfxVolume = value;

            sfxSource.volume = sfxVolume;

            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        }
    }

    public bool SFXIsMuted { get; private set; }
    public void SetSFXMute(bool mute)
    {
        SFXIsMuted = mute;
        PlayerPrefs.SetInt("SFXMute", SFXIsMuted ? 1 : 0);

        sfxSource.mute = SFXIsMuted;
    }

    private void Awake()
    {
        sfxSource = GetComponentInChildren<SFXSource>().GetComponent<AudioSource>();
        musicSource = GetComponentInChildren<MusicSource>().GetComponent<AudioSource>();

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

#if UNITY_EDITOR
[CustomEditor(typeof(VolumeManager))]
public class VolumeManagerInspector : Editor
{
    private float masterVolume = 0.75f;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        VolumeManager script = target as VolumeManager;
        
        if (Application.isPlaying)
        {
            masterVolume = script.MasterVolume;
            masterVolume = EditorGUILayout.Slider("Master Volume", masterVolume, 0f, 1f);
            script.MasterVolume = masterVolume;
        }
        else
        {
            masterVolume = EditorGUILayout.Slider("Master Volume", masterVolume, 0f, 1f);
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        }   
    }
}
#endif