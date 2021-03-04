using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaScript : MonoBehaviour
{
    public Color [] particleColor;
    private CharacterManager cm;
    private GameObject gachaMenu;
    private GameObject gachaSparkles;

    public int totalPulls;

    public bool inPull;


    [Header("Rates")]
    public int SR_Rate;
    public int R_Rate;
    public int C_Rate;


    void Start()
    {
        cm = GameObject.Find("GameManager").GetComponent<CharacterManager>();
        gachaMenu = GameObject.Find("Canvas").transform.Find("GachaPanel").gameObject;
        gachaSparkles = GameObject.Find("GachaSparkles");
        inPull = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            StartPull(false);
        }
    }

    void StartPull(bool tenPull = false)
    {
        if(!tenPull)
        {
            StartCoroutine("NormalPull");
        }
        else
        {
            for(int i = 0; i < 10; i++)
            {
                StartCoroutine("NormalPull");
            }
        }
    }

    void SkipPull()
    {
        StopCoroutine("NormalPull");
    }

    ParticleSystem SetParticleColor (string particleName, Color col)
    {
        ParticleSystem sys = gachaSparkles.transform.Find(particleName).GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psMain = sys.main;
        psMain.startColor = col;
        return sys;
    }

    IEnumerator NormalPull ()
    {
        if(!inPull)
        {
            inPull = true;
            CharacterBase charPulled = new CharacterBase();
            totalPulls++;
            int chance = Random.Range(0, 101);
            Debug.Log("[Hit] - " + chance + "/100");
            if (chance <= SR_Rate)
            {
                if (Random.Range(0, 4) == 3) //1/4 Chance to get sparkles instantly
                {

                }
                else
                {
                    SetParticleColor("Void", particleColor[0]).Play();
                    SetParticleColor("Void1", particleColor[0]).Play();
                    yield return new WaitForSeconds(3f);
                    SetParticleColor("Void2", particleColor[1]).Play();
                    SetParticleColor("Void3", particleColor[1]).Play();


                    SetParticleColor("VoidBurst", particleColor[1]).Play();
                    SetParticleColor("VoidBurst1", particleColor[1]).Play();
                    yield return new WaitForSeconds(3f);

                    SetParticleColor("Void2", particleColor[2]).Play();
                    SetParticleColor("Void3", particleColor[2]).Play();

                    SetParticleColor("VoidBurst", particleColor[2]).Play();
                    SetParticleColor("VoidBurst1", particleColor[2]).Play();
                    yield return new WaitForSeconds(3f);
                }
                charPulled = cm.AddRandomCharacter(3);
            }
            else if (chance <= R_Rate)
            {
                SetParticleColor("Void", particleColor[0]).Play();
                SetParticleColor("Void1", particleColor[0]).Play();
                yield return new WaitForSeconds(3f);
                SetParticleColor("Void2", particleColor[1]).Play();
                SetParticleColor("Void3", particleColor[1]).Play();

                SetParticleColor("VoidBurst", particleColor[1]).Play();
                SetParticleColor("VoidBurst1", particleColor[1]).Play();
                yield return new WaitForSeconds(3f);
                charPulled = cm.AddRandomCharacter(2);
            }
            else
            {
                //Common
                SetParticleColor("Void", particleColor[0]).Play();
                SetParticleColor("Void1", particleColor[0]).Play();
                yield return new WaitForSeconds(3f);
                charPulled = cm.AddRandomCharacter(1);
            }

            Debug.Log("A");
            if(charPulled.getSprite() != null)
                gachaMenu.transform.Find("CharacterSprite").GetComponent<Image>().sprite = charPulled.getSprite();
            inPull = false;
        }
    }  
}
