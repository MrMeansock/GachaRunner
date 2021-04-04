using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateScoreText : MonoBehaviour
{
    private TextMeshProUGUI tmpText;

    // Dependencies
    private ScoreHandler scoreHandler;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();

        scoreHandler = FindObjectOfType<ScoreHandler>();
    }

    private void Update()
    {
        tmpText.text = $"Score: {scoreHandler.Score}";
    }
}
