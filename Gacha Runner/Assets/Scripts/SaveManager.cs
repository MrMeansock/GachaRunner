using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private SaveState data;
    private CharacterManager cm;
    private MenuCurrencyHandler curManager;
    private string defaultPath;
    private int resetSafety;
    public bool allowSaving;

    private void Start()
    {
        resetSafety = 3;
        cm = this.GetComponent<CharacterManager>();
        curManager = GameObject.Find("MenuCurrencyManager").GetComponent<MenuCurrencyHandler>();
        //Default path
        if (Directory.Exists(Application.persistentDataPath))
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
        if (!allowSaving) return;
        FileExists();

        data = new SaveState(curManager.PlayerCurrency, cm.selectedCharacter, cm.userCharacters.ToArray());

        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(defaultPath, jsonData);
        Debug.Log("Saved to: " + defaultPath);
    }

    public void ResetProgress(GameObject button, CharacterPanel characterPanel)
    {
        resetSafety--;
        Debug.LogWarning("!!! - Reset in " + resetSafety);
        if (resetSafety == 0)
        {
            if (File.Exists(defaultPath))
            {
                File.Delete(defaultPath);
                characterPanel.Clear();
                Load();
            }
            resetSafety = 3;
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Reset (" + resetSafety + ")";
        }
        else
        {
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Reset (" + resetSafety + ")";
        }
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
        if (!allowSaving) return;
        if(FileExists() == false) 
        {
            //Spawn default character if list is empty
            cm.userCharacters = new List<CharacterBase>();
            cm.AddCharacter("Terry");

            Save();
            return;
        }

        string loadedJson = File.ReadAllText(defaultPath);
        data = JsonUtility.FromJson<SaveState>(loadedJson);

        //Do loading things here
        #region Loading Stuff

        curManager.PlayerCurrency = data.currency;
        cm.selectedCharacter = data.selectedCharacter;
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
