using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFrame : MonoBehaviour
{
    private GameObject player;
    private float lastPlayerX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lastPlayerX = player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(player.transform.position.x - lastPlayerX, 0, 0));
        lastPlayerX = player.transform.position.x;
    }
}
