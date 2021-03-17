using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    public GameObject characterSheet;
    private CharacterManager cm;

    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.Find("OverallGameManager").GetComponent<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            CreatePanel();
        }
    }

    public void CreatePanel()
    {
        Transform contentPanel = this.transform.GetChild(0).GetChild(0);
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        for(int temp = 0; temp < cm.userCharacters.Count; temp++)
        {
            GameObject csheet = Instantiate(characterSheet, this.transform.GetChild(0).GetChild(0)) as GameObject;
            csheet.GetComponent<CharacterSelectButton>().orderNum = temp;
            if(cm.userCharacters[temp].GetSprite() != null)
            csheet.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = cm.userCharacters[temp].GetSprite();
            csheet.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "LV: " + cm.userCharacters[temp].GetLevel();
        }
    }
}
