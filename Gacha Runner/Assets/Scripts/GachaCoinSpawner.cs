using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaCoinSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab = null;
    [SerializeField]
    private GameObject coinContainer = null;
    private BoxCollider2D spawnArea;
    private GameManager gm;

    private float minCoinSpawnDelay = 5.0f;
    private float maxCoinSpawnDelay = 10.0f;

    private float coinSpawnDelay;
    private float coinSpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        coinSpawnDelay = Random.Range(minCoinSpawnDelay, maxCoinSpawnDelay);
        coinSpawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinSpawnTimer += Time.deltaTime;

        //Spawn missiles if delay is finished
        if (coinSpawnTimer > coinSpawnDelay)
        {
            SpawnCoin();

            //Reset delay
            coinSpawnDelay = Random.Range(minCoinSpawnDelay, maxCoinSpawnDelay);
            coinSpawnTimer = 0;
        }
    }

    void SpawnCoin()
    {
        //Get a random position in the spawn area
        Vector2 coinPos = new Vector2(transform.position.x + Random.Range(-spawnArea.bounds.extents.x, spawnArea.bounds.extents.x),
            transform.position.y + Random.Range(-spawnArea.bounds.extents.y, spawnArea.bounds.extents.y));
        //Spawn Coin
        Instantiate(coinPrefab, coinPos, Quaternion.identity, coinContainer.transform);
    }
}
