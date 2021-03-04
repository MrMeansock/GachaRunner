﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int level;
    [SerializeField]
    private int rarity;
    [SerializeField]
    private Sprite sprite;

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
        this.rarity = baseStats.rarity;
        this.sprite = baseStats.sprite;
    }

    public Character ()
    {
        this.name = "Null";
        this.level = 0;
        this.baseSpeed = 1;
        this.baseJump = 0;
        this.baseHealth = 1;
        this.rarity = 0;
        this.sprite = null;
    }

    public int GetRarity()
    {
        return rarity;
    }

    public Sprite getSprite()
    {
        return sprite;
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
