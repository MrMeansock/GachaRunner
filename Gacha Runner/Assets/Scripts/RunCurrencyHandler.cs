using System;
using UnityEngine;

public class RunCurrencyHandler : MonoBehaviour
{
    private ScoreHandler scoreHandler;

    //[Tooltip("Score -> to currency conversion factor.")]
    //[SerializeField] int scoreToCurrency = 1;

    int runEarnings; // Currency the player has earned during the run (non score related, pickups?)
    public int RunEarnings
    {
        get => runEarnings;
        set
        {
            runEarnings = value;
            if (value > 0)
            {
                OnCurrencyEarned?.Invoke(value);
            }
        }
    }

    public event Action<int> OnCurrencyEarned;
    public event Action<int> OnEndOfRoundCurrency;

    private void Awake()
    {
        scoreHandler = FindObjectOfType<ScoreHandler>();
        RunEarnings = 0;
    }

    private void OnEnable()
    {
        scoreHandler.OnFinalScore += CalculateRunEarnings;
        GachaCoin.OnGachaCoinCollected += IncreaseRunEarnings;
    }

    private void OnDisable()
    {
        scoreHandler.OnFinalScore -= CalculateRunEarnings;
        GachaCoin.OnGachaCoinCollected -= IncreaseRunEarnings;
    }

    private void CalculateRunEarnings(int finalScore)
    {
        OnEndOfRoundCurrency?.Invoke(RunEarnings);
    }

    private void IncreaseRunEarnings(GachaCoin coin)
    {
        RunEarnings++;
    }
}
