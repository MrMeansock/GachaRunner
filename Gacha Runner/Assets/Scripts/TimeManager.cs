using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [HideInInspector] public float timeScale;

    void SetTimescale(float tScale)
    {
        Time.timeScale = tScale;
    }
}
