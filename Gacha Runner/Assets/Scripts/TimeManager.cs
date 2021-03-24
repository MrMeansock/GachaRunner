using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeScale;

    void SetTimescale(float tScale)
    {
        Time.timeScale = tScale;
    }

    private void Update()
    {
        SetTimescale(timeScale);
    }
}
