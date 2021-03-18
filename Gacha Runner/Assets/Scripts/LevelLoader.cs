using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private float groundHeight = 0;
    [SerializeField]
    private GameObject groundPrefab = null;
    private float groundWidth;
    private List<GameObject> groundObjects;
    private Character character;
    private float currHeight;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        groundWidth = groundPrefab.GetComponent<BoxCollider2D>().size.x * groundPrefab.transform.localScale.x;
        groundObjects = new List<GameObject>();
        groundObjects.Add(Instantiate(groundPrefab, new Vector3(character.transform.position.x - groundWidth, groundHeight, 0), groundPrefab.transform.rotation));
        currHeight = groundObjects[groundObjects.Count - 1].GetComponent<GenSection>().GenerateFlat(0.5f);
        groundObjects.Add(Instantiate(groundPrefab, new Vector3(character.transform.position.x, groundHeight, 0), groundPrefab.transform.rotation));
        currHeight = groundObjects[groundObjects.Count - 1].GetComponent<GenSection>().GenerateFlat(currHeight);
        groundObjects.Add(Instantiate(groundPrefab, new Vector3(character.transform.position.x + groundWidth, groundHeight, 0), groundPrefab.transform.rotation));
        currHeight = groundObjects[groundObjects.Count - 1].GetComponent<GenSection>().Generate(currHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if(character.transform.position.x > groundObjects[1].transform.position.x + (groundWidth / 2))
        {
            GameObject.Destroy(groundObjects[0]);
            groundObjects.RemoveAt(0);
            groundObjects.Add(Instantiate(groundPrefab, new Vector3(groundObjects[1].transform.position.x + groundWidth, groundHeight, 0), groundPrefab.transform.rotation));
            currHeight = groundObjects[groundObjects.Count - 1].GetComponent<GenSection>().Generate(currHeight);
        }
    }
}
