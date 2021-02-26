using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaScript : MonoBehaviour
{
    public Color particleColor;
    private CharacterManager cm;
    private GameObject gachaMenu;


    [Header("Rates")]
    public int SR_Rate;
    public int R_Rate;
    public int C_Rate;


    void Start()
    {
        cm = GameObject.Find("GameManager").GetComponent<CharacterManager>();
        gachaMenu = GameObject.Find("Canvas").transform.Find("GachaMenu").gameObject;
    }

    void Update()
    {
        
    }

    void StartHit(bool tenHit = false)
    {
        if(!tenHit)
        {
            StartCoroutine("NormalHit");
        }
        else
        {
            for(int i = 0; i < 10; i++)
            {
                StartCoroutine("NormalHit");
            }
        }
    }

    void SkipHit()
    {
        StopCoroutine("NormalHit");
    }

    IEnumerator NormalHit ()
    {

        int chance = Random.Range(0, 101);
        if(chance <= SR_Rate)
        {
            if(Random.Range(0, 4) == 3) //1/4 Chance to get sparkles instantly
            {
                
            }
            else
            {

            }
        }
        else if(chance <= R_Rate)
        {

        }
        else
        {
            //Common
            gachaMenu.transform.Find("Particles").GetChild(0).GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(5f);
        }
    }
}
