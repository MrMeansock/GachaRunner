using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private CharStats[] baseCharacters;
    public List<Character> userCharacters;
    // Start is called before the first frame update
    void Start()
    {

    }

    void AddCharacter (string name)
    {
        foreach(CharStats charstats in baseCharacters)
        {
            if (charstats.name == name)
            {
                userCharacters.Add(new Character(charstats));
                return;
            }
        }
        Debug.LogError("Name Was Not Found While Adding Character!");
    }



    void AddRandomCharacter()
    {
        userCharacters.Add(new Character(baseCharacters[Random.Range(0, baseCharacters.Length)]));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            AddRandomCharacter();
        }
    }
}
