using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private Menu pauseMenu = null;

    private bool paused;

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void SetPause(bool paused)
    {
        this.paused = paused;
        
        pauseMenu.gameObject.SetActive(paused);
        UpdateTimeScale(paused);
    }

    public void TogglePause()
    {
        paused = !paused;
        pauseMenu.gameObject.SetActive(paused);
        UpdateTimeScale(paused);
    }

    private void UpdateTimeScale(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
