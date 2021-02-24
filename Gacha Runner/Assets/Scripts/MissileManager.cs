using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject missilePrefab = null;
    private BoxCollider2D spawnArea;

    private float minMissileSpawnDelay = 1.5f;
    private float maxMissileSpawnDelay = 2.5f;
    private int minMissilesSpawned = 1;
    private int maxMissilesSpawned = 3;

    private float missileSpawnDelay;
    private float missileSpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        missileSpawnDelay = Random.Range(minMissileSpawnDelay, maxMissileSpawnDelay);
        missileSpawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        missileSpawnTimer += Time.deltaTime;

        //Spawn missiles if delay is finished
        if (missileSpawnTimer > missileSpawnDelay)
        {
            //Pick random amount of missiles to spawn
            int missilesToSpawn = Random.Range(minMissilesSpawned, maxMissilesSpawned);
            for(int i = 0; i < missilesToSpawn; i++)
            {
                SpawnMissile();
            }
            //Reset delay
            missileSpawnDelay = Random.Range(minMissileSpawnDelay, maxMissileSpawnDelay);
            missileSpawnTimer = 0;
        }
    }

    void SpawnMissile()
    {
        //Get a random position in the spawn area
        Vector2 missilePos = new Vector2(transform.position.x + Random.Range(-spawnArea.bounds.extents.x, spawnArea.bounds.extents.x),
            transform.position.y + Random.Range(-spawnArea.bounds.extents.y, spawnArea.bounds.extents.y));
        //Spawn missile
        Instantiate(missilePrefab, missilePos, Quaternion.identity);
    }
}
