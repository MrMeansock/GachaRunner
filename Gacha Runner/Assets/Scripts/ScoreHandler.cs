using System;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    // Dependencies
    private Character player;
    private RunEndHandler runEndHandler;

    [Header("Parameters")]
    [Tooltip("Displacement -> Score conversion factor.")]
    [SerializeField] int dispToScoreFactor = 100;

    public int Score { get; private set; }

    public event Action<int> OnFinalScore;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        runEndHandler = FindObjectOfType<RunEndHandler>();
    }

    private void OnEnable()
    {
        runEndHandler.OnRunEnd += PublishFinalScore;
    }

    private void OnDisable()
    {
        runEndHandler.OnRunEnd -= PublishFinalScore;
    }


    private void Update()
    {
        Score = CalculateScore(player.DisplacementX);
    }

    private int CalculateScore(float displacementX)
    {
        return (int)(player.DisplacementX * dispToScoreFactor);
    }

    private void PublishFinalScore()
    {
        int finalScore = CalculateScore(player.DisplacementX);
        OnFinalScore?.Invoke(finalScore);
    }
}
