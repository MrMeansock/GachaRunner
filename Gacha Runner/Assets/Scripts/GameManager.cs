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
    private GameObject gameOverMenu = null;
    private float gameTimer;

    //Progression values
    private float progPlayerSpeedMult = 0.02f; //Increase player speed by 0.2 per second
    private float progMissileSpeedMult = 0.01f; //Increse missile speed by 0.1 per second
    private float progMinMissileDelayMult = 0.25f; //Minimum amount of missile delay multiplier
    private float progBaseMissileDelayMult = 0.005f; //Rate at which missile delay decreases
                                                    //Smaller value = more gradual
    private int progMinMinMissilesToSpawn = 1; //Minimum amount of min missiles to spawn
    //private int progMaxMinMissilesToSpawn = 3; //Max amount of min missiles to spawn
    private float progMinMissilesIncreaseTime = 40.0f; //Amount of time to increase amount of min missiles
    private int progMinMaxMissilesToSpawn = 2; //Min amount of max missiles to spawn
    private float progMaxMissilesIncreaseTime = 25.0f; //Amount of time to increase amount of max missiles
    

    public float PlayerSpeedMultiplier
    {
        get { return 1 + gameTimer * progPlayerSpeedMult; }
    }

    public float MissileSpeedMultiplier
    {
        get { return 1 + gameTimer * progMissileSpeedMult; }
    }

    public float MissileDelayMultiplier
    {
        get { return (1 - progMinMissileDelayMult) / (progBaseMissileDelayMult * gameTimer + 1) + progMinMissileDelayMult; }
    }

    public int MinMissilesToSpawn
    {
        get { return progMinMinMissilesToSpawn + Mathf.FloorToInt(gameTimer / progMinMissilesIncreaseTime); }
    }

    public int MaxMissilesToSpawn
    {
        get { return progMinMaxMissilesToSpawn + Mathf.FloorToInt(gameTimer / progMaxMissilesIncreaseTime); }
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
        gameTimer = 0;
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;
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
