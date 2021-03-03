using System;
using UnityEngine;

public class CurrencyHandler : MonoBehaviour
{
    [SerializeField] ScoreHandler scoreHandler;

    [Tooltip("Score -> to currency conversion factor.")]
    [SerializeField] float scoreToCurrency = 0.1f;

    float runEarnings; // How much currency the player has earned during the run
    public float RunEarnings
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
        if (!scoreHandler) scoreHandler = FindObjectOfType<ScoreHandler>();
    }

    private void OnEnable()
    {
        scoreHandler.OnFinalScore += CalculateRunEarnings;
    }

    private void OnDisable()
    {
        scoreHandler.OnFinalScore -= CalculateRunEarnings;
    }

    private void CalculateRunEarnings(int finalScore)
    {
        float scoreEarnings = finalScore * scoreToCurrency;
        OnEndOfRoundCurrency?.Invoke(scoreEarnings + runEarnings);
    }
}
