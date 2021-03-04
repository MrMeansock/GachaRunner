using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPaused;
    public bool IsPaused => isPaused;
    private Character player;
    [SerializeField]
    private GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
    }

    public void GameOver()
    {
        isPaused = true;
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);

    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
