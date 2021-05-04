using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateCurrencyText : MonoBehaviour
{
    private TextMeshProUGUI tmpText;

    // Dependencies
    private RunCurrencyHandler currencyHandler;
    private MenuCurrencyHandler menuCurrencyHandler;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        GameObject currencyManager = GameObject.Find("CurrencyManager");

        currencyHandler = currencyManager.GetComponentInChildren<RunCurrencyHandler>();
        menuCurrencyHandler = currencyManager.GetComponentInChildren<MenuCurrencyHandler>();
    }

    private void Update()
    {
        tmpText.text = $"         : {menuCurrencyHandler.PlayerCurrency + currencyHandler.RunEarnings}";
    }
}
