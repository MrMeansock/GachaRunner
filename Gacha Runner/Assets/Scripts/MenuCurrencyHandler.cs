using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuCurrencyHandler : MonoBehaviour
{
    private const string PlayerCurrencyKey = "PLAYER_CURRENCY";

    private RunCurrencyHandler runCurrencyHandler;

    // Amount of money in player account
    public int PlayerCurrency
    {
        get => PlayerPrefs.GetInt(PlayerCurrencyKey);
        set
        {
            PlayerPrefs.SetInt(PlayerCurrencyKey, value);
        }
    }

    private void Awake()
    {
        if (!runCurrencyHandler) runCurrencyHandler = FindObjectOfType<RunCurrencyHandler>();
    }

    private void OnEnable()
    {
        if (runCurrencyHandler)
        {
            runCurrencyHandler.OnEndOfRoundCurrency += SaveRunCurrencyEarned;
        }
    }

    private void OnDisable()
    {
        if (runCurrencyHandler)
        {
            runCurrencyHandler.OnEndOfRoundCurrency -= SaveRunCurrencyEarned;
        }
    }

    private void SaveRunCurrencyEarned(int earned)
    {
        PlayerCurrency += earned;
    }


    // Stubbed buy method
    public void Buy(IBuyable buyable)
    {
        // if (playerCurrency >= price) {
        IBuyable bought = buyable.Buy();
        //}
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MenuCurrencyHandler))]
public class MenuCurrencyHandlerEditor : Editor
{
    int currency;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.LabelField("(For Debugging) Set your player currency", EditorStyles.boldLabel);
        currency = Mathf.Max(currency, 0);
        currency = EditorGUILayout.IntField("Currency: ", currency);

        if (GUILayout.Button("Set Player Currency"))
        {
            FindObjectOfType<MenuCurrencyHandler>().PlayerCurrency = currency;
        }
    }
}
#endif