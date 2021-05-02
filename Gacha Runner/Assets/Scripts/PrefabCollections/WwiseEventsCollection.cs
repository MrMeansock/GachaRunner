using UnityEngine;

public class WwiseEventsCollection : MonoBehaviour
{
    public AK.Wwise.Event OnGameStarted = null;
    public AK.Wwise.Event OnRarityUp = null;
    public AK.Wwise.Event OnMissileHit = null;
    public AK.Wwise.Event OnCoinCollected = null;
    public AK.Wwise.Event OnDeath = null;

    // UI
    public AK.Wwise.Event OnButtonClicked = null;
}
