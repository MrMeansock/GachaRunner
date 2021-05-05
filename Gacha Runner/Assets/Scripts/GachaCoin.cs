using System;
using UnityEngine;

public class GachaCoin : MonoBehaviour
{
    public static event Action<GachaCoin> OnGachaCoinCollected;
    private float lifeTime = 0.0f;

    private SFXCollection sfxCollection;
    private SoundManager soundManager;

    private void Awake()
    {
        sfxCollection = FindObjectOfType<SFXCollection>();
        soundManager = FindObjectOfType<SoundManager>();
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
            soundManager.PlaySFX(sfxCollection.coinCollect);
        }
    }

    public void Collect()
    {
        OnGachaCoinCollected?.Invoke(this);
        GameObject.Destroy(gameObject);
    }
}
