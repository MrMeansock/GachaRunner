using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    private SoundManager soundManager = null;
    private SFXCollection sfxCollection = null;
    private VolumeManager volumeManager = null;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        sfxCollection = FindObjectOfType<SFXCollection>();
        // volumeManager = FindObjectOfType<VolumeManager>();
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
        FindObjectOfType<VolumeManager>().MasterVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        FindObjectOfType<VolumeManager>().MusicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        FindObjectOfType<VolumeManager>().SFXVolume = volume;
    }

    public void SetMusicMute(bool mute)
    {
        FindObjectOfType<VolumeManager>().SetMusicMute(mute);
    }

    public void SetSFXMute(bool mute)
    {
        FindObjectOfType<VolumeManager>().SetSFXMute(mute);
    }

    #endregion
}
