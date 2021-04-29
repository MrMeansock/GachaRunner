using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaScript : MonoBehaviour
{
    public Color[] particleColor;
    private CharacterManager cm;
    private GameObject gachaMenu;
    private GameObject gachaSparkles;
    private GameObject characterSprite;

    private MenuCurrencyHandler menuCurrency;
    private AudioSource raritySFXSource;


    public int totalPulls;

    public bool inPull;

    // Cost of summoning
    public int cost = 10;

    [Header("Rates")]
    public int SR_Rate;
    public int R_Rate;
    public int C_Rate;

    private void Awake()
    {
        raritySFXSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        characterSprite = transform.Find("CharacterSprite").gameObject;
        cm = GameObject.Find("OverallGameManager").GetComponent<CharacterManager>();
        gachaMenu = GameObject.Find("Canvas").transform.Find("SummonPanel").gameObject;
        gachaSparkles = GameObject.Find("GachaSparkles");
        menuCurrency = FindObjectOfType<MenuCurrencyHandler>();
        inPull = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartPull(false);
        }
    }

    private void PlayRaritySound()
    {
        raritySFXSource.pitch = UnityEngine.Random.Range(1f, 2f);
        raritySFXSource.Play();
    }

    public void StartPull(bool tenPull = false)
    {
        if (!inPull)
        {

            if (!tenPull)
            {
                if (menuCurrency.PlayerCurrency - cost >= 0)
                {
                    StartCoroutine("NormalPull");
                }
                else
                {
                    Debug.Log("Not enough currency to summon!!");
                }
            }
            else
            {
                if (menuCurrency.PlayerCurrency - cost * 10 >= 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        StartCoroutine("NormalPull");
                    }
                }
                else
                {
                    Debug.Log("Not enough currency to summon ten!!");
                }
            }
        }
    }

    void SkipPull()
    {
        StopCoroutine("NormalPull");
    }

    ParticleSystem SetParticleColor(string particleName, Color col)
    {
        ParticleSystem sys = gachaSparkles.transform.Find(particleName).GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psMain = sys.main;
        psMain.startColor = col;
        return sys;
    }

    IEnumerator NormalPull()
    {
        if (!inPull && !gachaSparkles.transform.GetChild(0).GetComponent<ParticleSystem>().isPlaying)
        {
            menuCurrency.PlayerCurrency -= cost;

            inPull = true;
            characterSprite.SetActive(false);
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

                    PlayRaritySound();

                    SetParticleColor("VoidBurst", particleColor[1]).Play();
                    SetParticleColor("VoidBurst1", particleColor[1]).Play();
                    yield return new WaitForSeconds(3f);

                    SetParticleColor("Void2", particleColor[2]).Play();
                    SetParticleColor("Void3", particleColor[2]).Play();

                    PlayRaritySound();

                    SetParticleColor("VoidBurst", particleColor[2]).Play();
                    SetParticleColor("VoidBurst1", particleColor[2]).Play();
                    yield return new WaitForSeconds(3f);
                }

                PlayRaritySound();

                charPulled = cm.AddRandomCharacter(3);
            }
            else if (chance <= R_Rate)
            {
                SetParticleColor("Void", particleColor[0]).Play();
                SetParticleColor("Void1", particleColor[0]).Play();
                yield return new WaitForSeconds(3f);
                SetParticleColor("Void2", particleColor[1]).Play();
                SetParticleColor("Void3", particleColor[1]).Play();

                PlayRaritySound();

                SetParticleColor("VoidBurst", particleColor[1]).Play();
                SetParticleColor("VoidBurst1", particleColor[1]).Play();
                yield return new WaitForSeconds(3f);

                PlayRaritySound();

                charPulled = cm.AddRandomCharacter(2);
            }
            else
            {
                //Common
                SetParticleColor("Void", particleColor[0]).Play();
                SetParticleColor("Void1", particleColor[0]).Play();
                yield return new WaitForSeconds(3f);

                PlayRaritySound();

                charPulled = cm.AddRandomCharacter(1);
            }

            Debug.Log("A");
            if (charPulled.MainSprite != null)
            {
                characterSprite.SetActive(true);
                characterSprite.GetComponent<Image>().sprite = charPulled.MainSprite;
            }

            inPull = false;
        }
    }
}
