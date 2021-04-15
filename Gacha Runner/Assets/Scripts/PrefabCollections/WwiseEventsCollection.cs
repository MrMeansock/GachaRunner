using UnityEngine;

public class WwiseEventsCollection : MonoBehaviour
{
    public AK.Wwise.Event OnMainMenuLoad = null;
    public AK.Wwise.Event OnMainMenuUnload = null;
    public AK.Wwise.Event OnPlay = null;
    public AK.Wwise.Event OnPlayUnload = null;

    // UI
    public AK.Wwise.Event OnBackButtonClick = null;
    public AK.Wwise.Event OnButtonClick = null;
}
