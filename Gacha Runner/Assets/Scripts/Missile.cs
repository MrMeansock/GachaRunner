using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GotchaGuys.GameRectangle;

public class Missile : MonoBehaviour
{
    private GameObject player;
    private GameObject body;
    private float speed = 5.0f;
    private float lifeTime = 0f;
    private float offset;
    private float wiggleStrength = 10.0f;
    private float perlinMult = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = transform.GetChild(0).gameObject;
        offset = Random.Range(0, 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        //Move towards player
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        transform.Translate(dir * speed * Time.deltaTime);

        //Rotate body towards player
        body.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * 180 / (2 * 3.14159f) + 90);

        //Randomize missile movement with perline noise
        float perlinMod = Mathf.PerlinNoise((lifeTime + offset) * perlinMult, 0);
        perlinMod = perlinMod - 0.5f;
        transform.Translate(Vector2.up * perlinMod * wiggleStrength * Time.deltaTime, body.transform);

        lifeTime += Time.deltaTime;
    }

    public void SetSpeedMult(float speedMult)
    {
        speed *= speedMult;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Player has been hit
            collision.GetComponent<Character>().TakeDamage(true);
            GameObject.Destroy(gameObject);
        }
        if (collision.TryGetComponent<Rectangle>(out Rectangle rectangle))
        {
            // Missigle hit a rectangle
            GameObject.Destroy(gameObject);
        }
    }
}
