using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    private SoundManager soundManager = null;
    private SFXCollection sfxCollection = null;
    private VolumeManager volumeManager = null;


    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        sfxCollection = FindObjectOfType<SFXCollection>();
        volumeManager = FindObjectOfType<VolumeManager>();
    }

    #region Sound

    /// <summary>
    /// Plays the default button click sound by posting the corresponding Wwise event
    /// </summary>
    /// <param name="sender">The object that called the event</param>
    public void PlayButtonClick()
    {
        soundManager.PlaySFX(sfxCollection.buttonClick);
    }

    public void SetMasterVolume(float volume)
    {
        volumeManager.MasterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        volumeManager.MusicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        volumeManager.SFXVolume = volume;
    }

    public void SetMusicMute(bool mute)
    {
        volumeManager.SetMusicMute(mute);
    }

    public void SetSFXMute(bool mute)
    {
        volumeManager.SetSFXMute(mute);
    }

    #endregion
}
