using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGM : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManager = null;
    private void Awake()
    {
        GameObject gm = new GameObject();
        if (GameObject.Find("OverallGameManager") == null)
            gm = (GameObject) Instantiate(GameManager, Vector3.zero, Quaternion.identity);
        gm.name = "OverallGameManager";
    }
}
