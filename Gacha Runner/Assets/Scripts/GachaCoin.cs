using System;
using UnityEngine;

public class GachaCoin : MonoBehaviour
{
    public static event Action<GachaCoin> OnGachaCoinCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GachaCoin.OnGachaCoinCollected?.Invoke(this);
        if(collision.tag == "Player")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
