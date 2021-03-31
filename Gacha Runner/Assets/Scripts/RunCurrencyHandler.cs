using System;
using UnityEngine;

public class RunCurrencyHandler : MonoBehaviour
{
    private ScoreHandler scoreHandler;

    [Tooltip("Score -> to currency conversion factor.")]
    [SerializeField] int scoreToCurrency = 1;

    int runEarnings; // Currency the player has earned during the run (non score related, pickups?)
    public int RunEarnings
    {
        get => runEarnings;
        set
        {
            runEarnings += value;
            if (value > 0)
            {
                OnCurrencyEarned?.Invoke(value);
            }
        }
    }

    public event Action<float> OnCurrencyEarned;
    public event Action<float> OnEndOfRoundCurrency;

    private void Awake()
    {
        scoreHandler = FindObjectOfType<ScoreHandler>();
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
        float scoreEarnings = finalScore * scoreToCurrency;
        OnEndOfRoundCurrency?.Invoke(scoreEarnings + runEarnings);
    }

    private void IncreaseRunEarnings(GachaCoin coin)
    {
        RunEarnings++;
    }
}
