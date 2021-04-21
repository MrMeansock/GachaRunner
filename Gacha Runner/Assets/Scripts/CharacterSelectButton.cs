using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectButton : MonoBehaviour
{
    public int orderNum;
    public void SetCharacter()
    {
        GameObject.Find("OverallGameManager").GetComponent<CharacterManager>().selectedCharacter = orderNum;
        GameObject.Find("OverallGameManager").GetComponent<SaveManager>().Save();
    }
}
