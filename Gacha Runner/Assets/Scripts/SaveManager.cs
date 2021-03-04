using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private SaveState data;
    private CharacterManager cm;
    private string defaultPath;

    private void Start()
    {
        cm = this.GetComponent<CharacterManager>();

        //Default path
        if(Directory.Exists(Application.persistentDataPath))
        {
            defaultPath = Path.Combine(Application.persistentDataPath, "data.json");
        }
        else
        {
            defaultPath = Path.Combine(Application.streamingAssetsPath, "data.json");
        }
    }

    public void Save()
    {
        FileExists();

        data = new SaveState(cm.userCharacters.ToArray());

        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(defaultPath, jsonData);
        Debug.Log("Saved to: " + defaultPath);
    }

    bool FileExists()
    {
        if(!File.Exists(defaultPath))
        {
            File.Create(defaultPath).Close();
            return false;
        }
        return true;
    }

    public void Load ()
    {
        if(FileExists() == false) { return; }

        string loadedJson = File.ReadAllText(defaultPath);
        data = JsonUtility.FromJson<SaveState>(loadedJson);

        //Do loading things here
        #region Loading Stuff

        cm.userCharacters = new List<CharacterBase>(data.characters);

        #endregion

        Debug.Log("Loaded from: " + defaultPath);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            Save();
        }

        if(Input.GetKeyDown(KeyCode.F6))
        {
            Load();
        }
    }
}
