using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    private VolumeManager volumeManager = null;

    private AK.Wwise.Event onButtonClick = null;

    private void Awake()
    {
        volumeManager = FindObjectOfType<VolumeManager>();

        WwiseEventsCollection wwiseEvents = FindObjectOfType<WwiseEventsCollection>();
        onButtonClick = wwiseEvents.OnButtonClicked;
    }

    public void SetShopState()
    {
        WwiseSingleton.Instance.SetState("GameState", "InShop");
    }

    public void SetMenuState()
    {
        WwiseSingleton.Instance.SetState("GameState", "InMainMenu");
    }

    #region Sound

    /// <summary>
    /// Plays the default button click sound by posting the corresponding Wwise event
    /// </summary>
    /// <param name="sender">The object that called the event</param>
    public void PlayButtonClick()
    {
        AkSoundEngine.RegisterGameObj(gameObject);
        onButtonClick.Post(gameObject);
        AkSoundEngine.UnregisterGameObj(gameObject);
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
