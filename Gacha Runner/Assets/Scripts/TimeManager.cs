using UnityEngine;

public class TimeManager : MonoBehaviour
{
    /*[HideInInspector]*/ public float timeScale;

    void SetTimescale(float tScale)
    {
        timeScale = tScale;
        Time.timeScale = tScale;
    }

    private void Update()
    {
        Time.timeScale = timeScale;
    }
}
