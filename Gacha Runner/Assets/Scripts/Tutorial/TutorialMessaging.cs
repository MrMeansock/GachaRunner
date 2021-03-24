using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialMessaging : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private string[] messages = {
        "", // 0
        "Hello there", // 1
        "I see you are in a bit of a pickle", // 2
        "What do you say I lend you a hand?", // 3
        "Drag your finger across the screen to create a temporary shield", // 4
        "This will block incoming missile attacks", // 5
        "It can also be drawn under your feet to cross large gaps", // 6
        "Get creative with how you use the shields", // 7
        "And try to get as far as possible", // 8
    };

    [SerializeField] private float secondsPerLetter = 0.1f;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        Coroutine messagesCoroutine = StartCoroutine(DisplayMessages());
    }

    IEnumerator DisplayMessages()
    {
        yield return new WaitForSecondsRealtime(4);
        for (int i = 0; i < messages.Length; i++)
        {
            string msg = messages[i];
            tmp.text = msg;
            yield return new WaitForSecondsRealtime(msg.Length * secondsPerLetter);
        }

        tmp.text = "";
    }
}
