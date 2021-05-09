using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private CharStats[] baseCharacters = null;
    public List<CharacterBase> userCharacters;
    public int selectedCharacter;
    // Start is called before the first frame update
    void Start()
    {
        userCharacters = new List<CharacterBase>();
        this.GetComponent<SaveManager>().Load();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);


    }

    public void AddCharacter (string name)
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

    public void SortByLevel ()
    {
        userCharacters.Sort((p1, p2)=> p2.Level.CompareTo(p1.Level));
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
                    foreach(CharacterBase ch in userCharacters)
                    {
                        if(ch.Name == charToAdd.Name)
                        {
                            return charToAdd;
                        }
                    }
                    userCharacters.Add(charToAdd);
                    this.GetComponent<SaveManager>().Save();
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
            this.GetComponent<SaveManager>().Save();
            return charToAdd;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F4))
        {
            AddRandomCharacter(0);
            SortByLevel();
        }
    }
}
