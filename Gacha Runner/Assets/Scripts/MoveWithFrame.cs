using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFrame : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float lastPlayerX;
    private float trackPlayerX;

    // Start is called before the first frame update
    private void Awake()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");
        lastPlayerX = player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(player.transform.position.x - lastPlayerX, 0, 0));
        lastPlayerX = player.transform.position.x;
    }

    public void StartTracking()
    {
        trackPlayerX = player.transform.position.x;
    }

    public float GetTrackingDelta()
    {
        return player.transform.position.x - trackPlayerX;
    }
}
