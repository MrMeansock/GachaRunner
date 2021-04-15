using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CharStats : ScriptableObject
{
    public new string name;
    public int level;
    public int rarity;
    public Sprite mainSprite;
    //Movement
    public float baseSpeed;
    public float baseJump;
    public int baseHealth;
    //Game Variables
    public float invisFrames;
    public float downSlopeSpeed;
    public float upSlopeSpeed;
    public float maxPlatformLength;
    public float platformCooldown;
    public int powerID;
}
