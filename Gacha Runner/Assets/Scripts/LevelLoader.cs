using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private float groundHeight = 0;
    [SerializeField]
    private float parallaxFrontHeight = 0;
    [SerializeField]
    private float parallaxBackHeight = 0;
    [SerializeField]
    private float parallaxFrontSpeed = 0;
    [SerializeField]
    private float parallaxBackSpeed = 0;
    [SerializeField]
    private GameObject groundPrefab = null;
    [SerializeField]
    private GameObject parallaxFrontPrefab = null;
    [SerializeField]
    private GameObject parallaxBackPrefab = null;

    private float groundWidth;
    private float parallaxFrontWidth;
    private float parallaxBackWidth;

    private List<GameObject> groundObjects;
    private List<GameObject> parallaxFrontObjects;
    private List<GameObject> parallaxBackObjects;

    private Character character;
    private Vector3 characterPreviousPosition;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        characterPreviousPosition = character.transform.position;
        groundWidth = groundPrefab.GetComponent<BoxCollider2D>().size.x * groundPrefab.transform.localScale.x;
        parallaxFrontWidth = parallaxFrontPrefab.GetComponent<SpriteRenderer>().size.x * parallaxFrontPrefab.transform.localScale.x;
        parallaxBackWidth = parallaxBackPrefab.GetComponent<SpriteRenderer>().size.x * parallaxFrontPrefab.transform.localScale.x;
        groundObjects = new List<GameObject>();
        parallaxFrontObjects = new List<GameObject>();
        parallaxBackObjects = new List<GameObject>();
        groundObjects.Add(Instantiate(groundPrefab, new Vector3(character.transform.position.x - groundWidth, groundHeight, 0), groundPrefab.transform.rotation));
        groundObjects.Add(Instantiate(groundPrefab, new Vector3(character.transform.position.x, groundHeight, 0), groundPrefab.transform.rotation));
        groundObjects.Add(Instantiate(groundPrefab, new Vector3(character.transform.position.x + groundWidth, groundHeight, 0), groundPrefab.transform.rotation));
        parallaxFrontObjects.Add(Instantiate(parallaxFrontPrefab, new Vector3(character.transform.position.x - parallaxFrontWidth, parallaxFrontHeight, 0), parallaxFrontPrefab.transform.rotation));
        parallaxFrontObjects.Add(Instantiate(parallaxFrontPrefab, new Vector3(character.transform.position.x, parallaxFrontHeight, 0), parallaxFrontPrefab.transform.rotation));
        parallaxFrontObjects.Add(Instantiate(parallaxFrontPrefab, new Vector3(character.transform.position.x + parallaxFrontWidth, parallaxFrontHeight, 0), parallaxFrontPrefab.transform.rotation));
        parallaxBackObjects.Add(Instantiate(parallaxBackPrefab, new Vector3(character.transform.position.x - parallaxBackWidth, parallaxBackHeight, 0), parallaxBackPrefab.transform.rotation));
        parallaxBackObjects.Add(Instantiate(parallaxBackPrefab, new Vector3(character.transform.position.x, parallaxBackHeight, 0), parallaxBackPrefab.transform.rotation));
        parallaxBackObjects.Add(Instantiate(parallaxBackPrefab, new Vector3(character.transform.position.x + parallaxBackWidth, parallaxBackHeight, 0), parallaxBackPrefab.transform.rotation));
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterMoved())
        {
            MoveParallax();
        }

        if (character.transform.position.x > groundObjects[1].transform.position.x + (groundWidth / 2))
        {
            GameObject.Destroy(groundObjects[0]);
            groundObjects.RemoveAt(0);
            groundObjects.Add(Instantiate(groundPrefab, new Vector3(groundObjects[1].transform.position.x + groundWidth, groundHeight, 0), groundPrefab.transform.rotation));
        }

        if (IsOffscreen(parallaxFrontObjects[1].transform.position.x - (parallaxFrontWidth * 0.5f)))
        {
            GameObject.Destroy(parallaxFrontObjects[0]);
            parallaxFrontObjects.RemoveAt(0);
            parallaxFrontObjects.Add(Instantiate(parallaxFrontPrefab, new Vector3(parallaxFrontObjects[1].transform.position.x + parallaxFrontWidth, parallaxFrontHeight, 0), parallaxFrontPrefab.transform.rotation));
        }
       
        if (IsOffscreen(parallaxBackObjects[2].transform.position.x - (parallaxBackWidth * 0.5f)))
        {
            GameObject.Destroy(parallaxBackObjects[0]);
            parallaxBackObjects.RemoveAt(0);
            parallaxBackObjects.Add(Instantiate(parallaxBackPrefab, new Vector3(parallaxBackObjects[1].transform.position.x + parallaxBackWidth, parallaxBackHeight, 0), parallaxBackPrefab.transform.rotation));
        }

        characterPreviousPosition = character.transform.position;
    }

    bool IsOffscreen(float x)
    {
        return x < MainCamera.ScreenToWorldPoint(Vector3.forward).x; 
    }

    bool CharacterMoved()
    {
        return !Mathf.Approximately(character.transform.position.x, characterPreviousPosition.x);
    }

    void MoveParallax()
    {
        parallaxFrontObjects.ForEach(pf =>
        {
            pf.transform.position += new Vector3(-parallaxFrontSpeed, 0);
        });

        parallaxBackObjects.ForEach(pb =>
        {
            pb.transform.position += new Vector3(-parallaxBackSpeed, 0);
        });
    }
}
