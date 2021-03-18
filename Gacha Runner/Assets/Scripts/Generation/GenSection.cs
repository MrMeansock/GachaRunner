using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenSection : MonoBehaviour
{
    //Config
    const float MAX_HEIGHT_DELTA = 0.2f;
    const float MIN_GAP_LENGTH = 0.05f;
    const float MAX_GAP_LENGTH = 0.2f;
    const float MIN_PLATFORM_LENGTH = 0.2f;
    const float MAX_PLATFORM_LENGTH = 1.0f;

    private BoxCollider2D bounds;
    [SerializeField]
    private GameObject genGroundPrefab;
    private float minY;
    [SerializeField]
    private float minX;
    private float maxY;
    private float maxX;
    private float xLen;
    private float yLen;

    private float currHeight;
    private float genProgress;

    private void Awake()
    {
        bounds = GetComponent<BoxCollider2D>();
        minY = bounds.bounds.min.y;
        minX = bounds.bounds.min.x;
        maxY = bounds.bounds.max.y;
        maxX = bounds.bounds.max.x;
        xLen = maxX - minX;
        yLen = maxY - minY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Generates a section of terrain
    /// </summary>
    /// <param name="height">Starting height between 0-1</param>
    /// <returns>Ending height between 0-1</returns>
    public float Generate(float height)
    {
        currHeight = height;
        genProgress = 0;
        while(genProgress < 1.0f)
        {
            float platformLen = Random.Range(MIN_PLATFORM_LENGTH, MAX_PLATFORM_LENGTH);
            //If platform length will exceed bounds
            if(genProgress + platformLen > 1.0f)
            {
                //Clamp platform, generate it then return
                platformLen = 1 - genProgress;
                AddGenGround(genProgress, currHeight, platformLen);
                break;
            }
            AddGenGround(genProgress, currHeight, platformLen);
            genProgress += platformLen;
            
            //Create a gap of random length
            float gapLen = Random.Range(MIN_GAP_LENGTH, MAX_GAP_LENGTH);
            genProgress += gapLen;

            //Change Height
            float deltaHeight = 0;
            if(currHeight == 1.0f)
            {
                deltaHeight = Random.Range(-MAX_HEIGHT_DELTA, 0);
            }
            else if(currHeight == 0)
            {
                deltaHeight = Random.Range(0, MAX_HEIGHT_DELTA);
            }
            else
            {
                deltaHeight = Random.Range(-MAX_HEIGHT_DELTA, MAX_HEIGHT_DELTA);
            }
            currHeight = Mathf.Clamp(currHeight + deltaHeight, 0, 1.0f);
        }

        return currHeight;
    }

    /// <summary>
    /// Generates a full flat section
    /// </summary>
    /// <param name="height">Starting height</param>
    /// <returns>Ending height</returns>
    public float GenerateFlat(float height)
    {
        AddGenGround(0, height, 1.0f);
        return height;
    }

    public void AddGenGround(float xpos, float height, float length)
    {
        GenGround newground = Instantiate(genGroundPrefab/*, transform*/).GetComponent<GenGround>();
        newground.transform.position = new Vector3(minX, minY);
        newground.SetWidth(length);
        newground.transform.position = new Vector3(newground.transform.position.x + xpos * xLen, newground.transform.position.y + height * yLen);
    }
}
