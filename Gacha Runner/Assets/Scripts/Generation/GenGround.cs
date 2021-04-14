using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenGround : MonoBehaviour
{
    private const float BASE_WIDTH = 2.56f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D col;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWidth(float size)
    {
        spriteRenderer.size = new Vector2(size * BASE_WIDTH, spriteRenderer.size.y);
        col.size = new Vector2(size * BASE_WIDTH, col.size.y);
        //Move ground back to match original left edge
        transform.Translate(new Vector2(size * BASE_WIDTH / 2.0f * transform.localScale.x, 0));
    }
}
