using System;
using UnityEngine;

public class GachaCoin : MonoBehaviour
{
    public static event Action<GachaCoin> OnGachaCoinCollected;
    private float lifeTime = 0.0f;

    private AK.Wwise.Event onCoinCollected = null;

    private void Awake()
    {
        WwiseEventsCollection wwiseEvents = FindObjectOfType<WwiseEventsCollection>();
        onCoinCollected = wwiseEvents.OnCoinCollected;
    }

    private void Update()
    {
        if (lifeTime >= 10.0f)
        {
            Destroy(gameObject);
        }
        lifeTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Collect();
            AkSoundEngine.RegisterGameObj(gameObject);
            onCoinCollected.Post(gameObject);
            AkSoundEngine.UnregisterGameObj(gameObject);
        }
    }

    public void Collect()
    {
        OnGachaCoinCollected?.Invoke(this);
        GameObject.Destroy(gameObject);
    }
}
