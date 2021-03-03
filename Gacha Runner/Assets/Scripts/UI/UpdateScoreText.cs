using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateScoreText : MonoBehaviour
{
    TextMeshProUGUI tmpText;

    [SerializeField] ScoreHandler scoreHandler;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();

        if (!scoreHandler) scoreHandler = FindObjectOfType<ScoreHandler>();
    }

    private void Update()
    {
        tmpText.text = $"Score: {scoreHandler.Score}";
    }
}
