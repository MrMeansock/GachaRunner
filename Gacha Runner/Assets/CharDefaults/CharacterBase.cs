using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterBase
{
    [SerializeField]
    private string name;
    public string Name => name;
    [SerializeField]
    private int level;
    public int Level => level;
    [SerializeField]
    private int rarity;
    public int Rarity => rarity;
    [SerializeField]
    private Sprite mainSprite;
    public Sprite MainSprite => mainSprite;

    //Movement
    [SerializeField]
    private float baseSpeed;
    public float BaseSpeed => baseSpeed;
    [SerializeField]
    private float baseJump;
    public float BaseJump => baseJump;
    [SerializeField]
    private int baseHealth;
    public int BaseHealth => baseHealth;

    //Gameplay
    [SerializeField]
    private float invisFrames;
    public float InvisFrames => invisFrames;
    [SerializeField]
    private float slopeSpeed;
    public float SlopeSpeed => slopeSpeed;
    [SerializeField]
    private float maxPlatformLength;
    public float MaxPlatformLength => maxPlatformLength;
    [SerializeField]
    private float platformCooldown;
    public float PlatformCooldown => platformCooldown;
    [SerializeField]
    private int powerID;
    public int PowerID => powerID;

    public CharacterBase(CharStats baseStats)
    {
        this.name = baseStats.name;
        this.level = baseStats.level;
        this.mainSprite = baseStats.mainSprite; //Yo does this work
        this.baseSpeed = baseStats.baseSpeed;
        this.baseJump = baseStats.baseJump;
        this.baseHealth = baseStats.baseHealth;
        this.rarity = baseStats.rarity;
        this.invisFrames = baseStats.invisFrames;
        this.slopeSpeed = baseStats.slopeSpeed;
        this.maxPlatformLength = baseStats.maxPlatformLength;
        this.platformCooldown = baseStats.platformCooldown;
        this.powerID = baseStats.powerID;
    }

    public CharacterBase()
    {
        this.name = "Null";
        this.level = 0;
        this.baseSpeed = 1;
        this.baseJump = 0;
        this.baseHealth = 1;
        this.rarity = 0;
        this.mainSprite = null;
        this.invisFrames = 0.5f;
        this.slopeSpeed = 1;
        this.maxPlatformLength = 1;
        this.platformCooldown = 0.25f;
        this.powerID = 0;
    }
}
