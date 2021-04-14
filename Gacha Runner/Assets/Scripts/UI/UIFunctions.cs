using UnityEngine;

public class UIFunctions : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event onButtonClick;
    [SerializeField] private AK.Wwise.Event onBackButtonClick;

    /// <summary>
    /// Plays the default button click sound by posting the corresponding Wwise event
    /// </summary>
    /// <param name="sender">The object that called the event</param>
    public void PlayButtonClick(GameObject sender)
    {
        onButtonClick.Post(sender);
    }

    /// <summary>
    /// Plays the default back button click sound by posting the corresponding Wwise event
    /// </summary>
    /// <param name="sender">The object that called the event</param>
    public void PlayBackButtonClick(GameObject sender)
    {
        onBackButtonClick.Post(sender);
    }
}
