using UnityEngine;

public class TestFreeMoney : MonoBehaviour
{
    private MenuCurrencyHandler currencyHandler;

    private void Awake()
    {
        currencyHandler = FindObjectOfType<MenuCurrencyHandler>();
    }

    public void Motherlode()
    {
        currencyHandler.PlayerCurrency += 10;
    }
}
