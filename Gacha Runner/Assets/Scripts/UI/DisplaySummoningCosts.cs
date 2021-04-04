using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplaySummoningCosts : MonoBehaviour
{
    GachaScript gachaScript;
    TextMeshProUGUI tmp;

    private void Awake()
    {
        if (!gachaScript) gachaScript = FindObjectOfType<GachaScript>();
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        tmp.text = "Cost: " + gachaScript.cost;
    }
}
