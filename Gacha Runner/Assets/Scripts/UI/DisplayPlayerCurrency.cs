using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DisplayPlayerCurrency : MonoBehaviour
{
    MenuCurrencyHandler menuCurrency;
    TextMeshProUGUI tmp;

    private void Awake()
    {
        menuCurrency = FindObjectOfType<MenuCurrencyHandler>();
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        tmp.text = "Currency: " + menuCurrency.PlayerCurrency;
    }
}
