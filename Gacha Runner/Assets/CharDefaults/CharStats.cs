using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CharStats : ScriptableObject
{
    public new string name;
    public int level;
    public int rarity;
    public Sprite sprite;
    //Movement
    public float baseSpeed;
    public float baseJump;
    public float baseHealth;

}
