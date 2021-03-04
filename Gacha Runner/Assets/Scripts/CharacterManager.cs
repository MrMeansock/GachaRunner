using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private CharStats[] baseCharacters;
    public List<CharacterBase> userCharacters;
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
                userCharacters.Add(new CharacterBase(charstats));
                return;
            }
        }
        Debug.LogError("Name Was Not Found While Adding Character!");
    }



    public CharacterBase AddRandomCharacter(int r)
    {
        if(r != 0)
        {
            for(int i = Random.Range(0, baseCharacters.Length); i > -2; i = Random.Range(0, baseCharacters.Length))
            {
                if(baseCharacters[i].rarity == r)
                {
                    CharacterBase charToAdd = new CharacterBase(baseCharacters[i]);
                    userCharacters.Add(charToAdd);
                    return charToAdd;
                }
            }
            return null;
        }
        else
        {
            //if peram is 0
            CharacterBase charToAdd = new CharacterBase(baseCharacters[Random.Range(0, baseCharacters.Length)]);
            userCharacters.Add(charToAdd);
            return charToAdd;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            AddRandomCharacter(0);
        }
    }
}
