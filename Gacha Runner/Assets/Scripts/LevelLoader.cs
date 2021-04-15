using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private float groundHeight = 0;
    [Tooltip("Prefabs that will be loaded before generating random levels")]
    [SerializeField]
    private List<GameObject> directLoadLevels;
    private int directLevelsLoaded = 0;
    [Tooltip("Height of the last directly loaded. Used to start generating new levels")]
    [SerializeField]
    private float lastLoadHeight;
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
    [SerializeField]
    private List<Sprite> backgroundSprites;
    [SerializeField]
    private GameObject backgroundPrefab;
    [SerializeField]
    private float backgroundHeight;
    private List<GameObject> backgroundObjects;

    private float groundWidth;
    private float backgroundWidth;
    private float parallaxFrontWidth;
    private float parallaxBackWidth;

    private List<GameObject> groundObjects;
    private List<GameObject> parallaxFrontObjects;
    private List<GameObject> parallaxBackObjects;

    private Character character;
    private float currHeight;
    private Vector3 characterPreviousPosition;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        characterPreviousPosition = character.transform.position;
        groundWidth = groundPrefab.GetComponent<BoxCollider2D>().size.x * groundPrefab.transform.localScale.x;
        backgroundWidth = backgroundPrefab.GetComponent<BoxCollider2D>().size.x * backgroundPrefab.transform.localScale.x;
        parallaxFrontWidth = parallaxFrontPrefab.GetComponent<SpriteRenderer>().size.x * parallaxFrontPrefab.transform.localScale.x;
        parallaxBackWidth = parallaxBackPrefab.GetComponent<SpriteRenderer>().size.x * parallaxFrontPrefab.transform.localScale.x;
        groundObjects = new List<GameObject>();
        parallaxFrontObjects = new List<GameObject>();
        parallaxBackObjects = new List<GameObject>();
        backgroundObjects = new List<GameObject>();
        //Generate ground
        groundObjects.Add(Instantiate(groundPrefab, new Vector3(character.transform.position.x - groundWidth, groundHeight, 0), groundPrefab.transform.rotation));
        currHeight = groundObjects[groundObjects.Count - 1].GetComponent<GenSection>().GenerateFlat(0.5f);
        groundObjects.Add(Instantiate(groundPrefab, new Vector3(character.transform.position.x, groundHeight, 0), groundPrefab.transform.rotation));
        currHeight = groundObjects[groundObjects.Count - 1].GetComponent<GenSection>().GenerateFlat(currHeight);
        if (directLoadLevels.Count > directLevelsLoaded)
        {
            groundObjects.Add(Instantiate(directLoadLevels[directLevelsLoaded], new Vector3(character.transform.position.x + groundWidth, groundHeight, 0), directLoadLevels[directLevelsLoaded].transform.rotation));
            directLevelsLoaded++;
            currHeight = lastLoadHeight;
        }
        else
        {
            groundObjects.Add(Instantiate(groundPrefab, new Vector3(character.transform.position.x + groundWidth, groundHeight, 0), groundPrefab.transform.rotation));
            currHeight = groundObjects[groundObjects.Count - 1].GetComponent<GenSection>().Generate(currHeight);
        }
        parallaxFrontObjects.Add(Instantiate(parallaxFrontPrefab, new Vector3(character.transform.position.x - parallaxFrontWidth, parallaxFrontHeight, 6), parallaxFrontPrefab.transform.rotation));
        parallaxFrontObjects.Add(Instantiate(parallaxFrontPrefab, new Vector3(character.transform.position.x, parallaxFrontHeight, 6), parallaxFrontPrefab.transform.rotation));
        parallaxFrontObjects.Add(Instantiate(parallaxFrontPrefab, new Vector3(character.transform.position.x + parallaxFrontWidth, parallaxFrontHeight, 6), parallaxFrontPrefab.transform.rotation));
        parallaxBackObjects.Add(Instantiate(parallaxBackPrefab, new Vector3(character.transform.position.x - parallaxBackWidth, parallaxBackHeight, 6), parallaxBackPrefab.transform.rotation));
        parallaxBackObjects.Add(Instantiate(parallaxBackPrefab, new Vector3(character.transform.position.x, parallaxBackHeight, 6), parallaxBackPrefab.transform.rotation));
        parallaxBackObjects.Add(Instantiate(parallaxBackPrefab, new Vector3(character.transform.position.x + parallaxBackWidth, parallaxBackHeight, 6), parallaxBackPrefab.transform.rotation));
        AddBackgroundObject(new Vector3(character.transform.position.x - backgroundWidth * 2, backgroundHeight, 5));
        AddBackgroundObject(new Vector3(character.transform.position.x - backgroundWidth, backgroundHeight, 5));
        AddBackgroundObject(new Vector3(character.transform.position.x, backgroundHeight, 5));
        AddBackgroundObject(new Vector3(character.transform.position.x + backgroundWidth, backgroundHeight, 5));
        AddBackgroundObject(new Vector3(character.transform.position.x + backgroundWidth * 2, backgroundHeight, 5));
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
            if (directLoadLevels.Count > directLevelsLoaded)
            {
                groundObjects.Add(Instantiate(directLoadLevels[directLevelsLoaded], new Vector3(character.transform.position.x + groundWidth, groundHeight, 0), directLoadLevels[directLevelsLoaded].transform.rotation));
                directLevelsLoaded++;
            }
            else
            {
                groundObjects.Add(Instantiate(groundPrefab, new Vector3(groundObjects[1].transform.position.x + groundWidth, groundHeight, 0), groundPrefab.transform.rotation));
                currHeight = groundObjects[groundObjects.Count - 1].GetComponent<GenSection>().Generate(currHeight);
            }
        }

        if(character.transform.position.x > backgroundObjects[2].transform.position.x + (backgroundWidth / 2))
        {
            Destroy(backgroundObjects[0]);
            backgroundObjects.RemoveAt(0);
            AddBackgroundObject(new Vector3(backgroundObjects[backgroundObjects.Count-1].transform.position.x + backgroundWidth, backgroundHeight, 5));
        }

        if (IsOffscreen(parallaxFrontObjects[1].transform.position.x - (parallaxFrontWidth * 0.5f)))
        {
            GameObject.Destroy(parallaxFrontObjects[0]);
            parallaxFrontObjects.RemoveAt(0);
            parallaxFrontObjects.Add(Instantiate(parallaxFrontPrefab, new Vector3(parallaxFrontObjects[1].transform.position.x + parallaxFrontWidth, parallaxFrontHeight, 6), parallaxFrontPrefab.transform.rotation));
        }
       
        if (IsOffscreen(parallaxBackObjects[2].transform.position.x - (parallaxBackWidth * 0.5f)))
        {
            GameObject.Destroy(parallaxBackObjects[0]);
            parallaxBackObjects.RemoveAt(0);
            parallaxBackObjects.Add(Instantiate(parallaxBackPrefab, new Vector3(parallaxBackObjects[1].transform.position.x + parallaxBackWidth, parallaxBackHeight, 6), parallaxBackPrefab.transform.rotation));
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

    void AddBackgroundObject(Vector3 pos)
    {
        backgroundObjects.Add(Instantiate(backgroundPrefab, pos, backgroundPrefab.transform.rotation));
        //Select random sprite to use
        backgroundObjects[backgroundObjects.Count - 1].GetComponent<SpriteRenderer>().sprite = backgroundSprites[Random.Range(0, backgroundSprites.Count)];

    }
}
