using UnityEngine;

public class MenuCurrencyHandler : MonoBehaviour
{
    int playerCurrency; // Amount of money in player account

    // Stubbed buy method
    void Buy(IBuyable buyable)
    {
        // if (playerCurrency >= price) {
        IBuyable bought = buyable.Buy();
        //}
    }
}
