using System;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Character player;
    [SerializeField] RunEndHandler runEndHandler;

    [Header("Parameters")]
    [Tooltip("Displacement -> Score conversion factor.")]
    [SerializeField] int dispToScoreFactor = 100;

    public int Score { get; private set; }

    public event Action<int> OnFinalScore;

    private void Awake()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        if (!runEndHandler) runEndHandler = FindObjectOfType<RunEndHandler>();
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
