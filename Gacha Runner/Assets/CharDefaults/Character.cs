using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int level;

    //Movement
    [SerializeField]
    private float baseSpeed;
    [SerializeField]
    private float baseJump;
    [SerializeField]
    private float baseHealth;

    public Character(CharStats baseStats)
    {
        this.name = baseStats.name;
        this.level = baseStats.level;
        this.baseSpeed = baseStats.baseSpeed;
        this.baseJump = baseStats.baseJump;
        this.baseHealth = baseStats.baseHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
