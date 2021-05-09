using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Rigidbody2D rb;
    [SerializeField]
    protected GameObject body;
    protected GameManager gm;
    protected ActiveAbilities activeAbilities;
    protected CharacterBase selectedCharacter;

    //Other values
    protected bool grounded = false;
    protected float bounceTime = 0.0f;
    protected float gravity = 5.0f;
    protected float startX;

    //Config values
    [SerializeField]
    protected float speed = 10f;
    protected float bounceHeight = 0.25f;
    protected float bounceInterval = 0.3f;
    protected int powerID;

    //Invinciblity values
    protected float iMaxTime = 0.5f;
    protected float iTime = 0.0f;
    protected int iFrames = 0;
    protected bool isInvincible = false;

    //Health
    protected int maxHealth = 3;
    protected int health = 3;
    [SerializeField]
    protected GameObject healthArea;
    [SerializeField] protected bool godMode = false;

    //Slope values
    protected float bottomDisplacement = 0.5f;
    protected float headDisplacement = 0.5f;
    protected float slopeRayLength = 0.5f;
    protected float upSlopeForceMultiplier = 4.0f;
    protected float downSlopeForceMultiplier = 10.0f;
    protected float prevX = 0;
    protected float minXDelta = 0;
    protected float jumpStrength = 500.0f;
    protected float jumpRayLength = 1.0f;

    // Sound
    protected SFXCollection sfxCollection;
    protected SoundManager soundManager;

    //Effect boost
    protected float speedBoost = 1.0f;
    public float SpeedBoost
    {
        get { return speedBoost; }
        set { speedBoost = value; }
    }
    protected bool invulnerabilityBoost = false;
    public bool InvulnerabilityBoost
    {
        get { return invulnerabilityBoost; }
        set { invulnerabilityBoost = value; }
    }


    public float DisplacementX => Math.Abs(transform.position.x - startX); // (Abs to get pure distance, left or right).

    public event Action OnDeath;

    [SerializeField]
    private GotchaGuys.GameRectangle.MakeRectanglePreviews rectPreview = null;

    private void Awake()
    {
        sfxCollection = FindObjectOfType<SFXCollection>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        activeAbilities = GetComponent<ActiveAbilities>();

        startX = transform.position.x;
        prevX = transform.position.x;

        for(int i = healthArea.transform.childCount - 1; i > health - 1; i--)
        {
            healthArea.transform.GetChild(i).gameObject.SetActive(false);
        }

        activeAbilities.SetActiveSkill(SkillType.ClearMissiles);

        //Assign Values from selected character
        if (GameObject.Find("OverallGameManager") != null)
            GetCharValues();

    }

    protected void GetCharValues()
    {
        GameObject OGM = GameObject.Find("OverallGameManager").gameObject;
        selectedCharacter = OGM.GetComponent<CharacterManager>().userCharacters[OGM.GetComponent<CharacterManager>().selectedCharacter];
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = selectedCharacter.MainSprite;
       
        this.speed = selectedCharacter.BaseSpeed;
        this.jumpStrength = selectedCharacter.BaseJump;
        this.maxHealth = selectedCharacter.BaseHealth;

        this.iMaxTime = selectedCharacter.InvisFrames;
        this.downSlopeForceMultiplier = selectedCharacter.DownSlopeSpeed;
        this.upSlopeForceMultiplier = selectedCharacter.UpSlopeSpeed;
        rectPreview.MaxLength = selectedCharacter.MaxPlatformLength;
        rectPreview.Cooldown = selectedCharacter.PlatformCooldown;
        this.powerID = selectedCharacter.PowerID;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        MoveForward();
        HandleSlopes();
        if (transform.position.y < -6.0f && health > 0)
            TakeDamage(false);
        prevX = transform.position.x;
        if (Input.GetKeyDown(KeyCode.Space))
            activeAbilities.ActivateSkill();
    }

    protected void FixedUpdate()
    {
        HandleInvinciblity();
    }

    protected void MoveForward()
    {
        Vector2 force = transform.right * speed * gm.PlayerSpeedMultiplier * speedBoost;
        force += Vector2.down * gravity;
        rb.velocity = force;
        Bounce(true);
    }

    public Vector3 GetFuturePosition(float time)
    {
        return transform.position + transform.right * rb.velocity.x * time;
    }

    protected void HandleInvinciblity()
    {
        if(isInvincible || invulnerabilityBoost)
        {
            //Do invincilibity timer
            if(iTime >= iMaxTime && !invulnerabilityBoost)
            {
                isInvincible = false;
                //Make sure body is visible upon ending iFrames
                SpriteRenderer bodySprite = body.GetComponent<SpriteRenderer>();
                bodySprite.color = new Color(bodySprite.color.r, bodySprite.color.g, bodySprite.color.b, 1.0f);
                iTime = 0;
                iFrames = 0;
                return;
            }

            //Make body blink while invincible
            //Debug.Log(iFrames);
            if(iFrames % 5 == 0)
            {
                SpriteRenderer bodySprite = body.GetComponent<SpriteRenderer>();
                bodySprite.color = new Color(bodySprite.color.r, bodySprite.color.g, bodySprite.color.b, bodySprite.color.a == 0.0f ? 1.0f : 0.0f);
            }

            iTime += Time.deltaTime;
            iFrames++;
        }
    }

    protected void HandleSlopes()
    {
        RaycastHit2D[] hits = new RaycastHit2D[16];
        bool hitFound = false;
        bool slopeHitFound = false;
        float upSlopeForce = 0.0f;
        int hitAmount = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - bottomDisplacement), Vector2.right, new ContactFilter2D(), hits, slopeRayLength);
        
        for(int i = 0; i < hitAmount; i++)
        {
            if(hits[i].collider.tag != "Player")
            {
                hitFound = true;
                slopeHitFound = true;
                upSlopeForce = hits[i].normal.y;
                if (upSlopeForce > 0)
                {
                    upSlopeForce *= upSlopeForceMultiplier;
                }
            }
        }
        bool bottomHitFound = false;
    
        hitAmount = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - bottomDisplacement), Vector2.down, new ContactFilter2D(), hits, jumpRayLength);

        for (int i = 0; i < hitAmount; i++)
        {
            if (hits[i].collider.tag != "Player")
            {
                bottomHitFound = true;
                if (!hitFound)
                {
                    hitFound = true;
                    float upForce = hits[i].normal.y;
                    if (upForce > 0 && hits[i].normal.x < 0f)
                    {
                        upForce *= upSlopeForceMultiplier;
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + upForce);
                    }
                    else if (hits[i].normal.x > 0f)
                    {
                        float forwardForce = hits[i].normal.x;
                        forwardForce *= downSlopeForceMultiplier;
                        rb.velocity = new Vector2(rb.velocity.x + forwardForce, rb.velocity.y);
                    }
                }
            }
        }

        if(slopeHitFound && bottomHitFound)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + upSlopeForce);
        }

        bool rightHitFound = false;
        hitAmount = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + headDisplacement), Vector2.right, new ContactFilter2D(), hits, jumpRayLength);
        for (int i = 0; i < hitAmount; i++)
        {
            if (hits[i].collider.tag != "Player")
            {
                rightHitFound = true;
            }
        }

        //If x movement is too low, try to jump over obstical
        if (transform.position.x - prevX < minXDelta)
        {
            if (!rightHitFound && bottomHitFound)
                rb.AddForce(Vector2.up * jumpStrength);
        }

        if (!bottomHitFound && (rightHitFound || slopeHitFound))
            rb.velocity = new Vector2(0, rb.velocity.y);

        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - bottomDisplacement), new Vector3(transform.position.x + slopeRayLength, transform.position.y - bottomDisplacement), Color.red);
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + headDisplacement), new Vector3(transform.position.x + jumpRayLength, transform.position.y + headDisplacement), Color.blue);
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - bottomDisplacement), new Vector3(transform.position.x, transform.position.y - bottomDisplacement - jumpRayLength), Color.green);
    }

    public void TakeDamage(bool doInvincibility)
    {
        if (!godMode)
        {
            if (!isInvincible && !(invulnerabilityBoost && doInvincibility))
            {
                //Handle damage taking
                health--;
                if (health >= 0)
                {
                    healthArea.transform.GetChild(health).gameObject.SetActive(false);
                }
                if (health <= 0)
                {
                    soundManager.PlaySFX(sfxCollection.playerDeath);
                    // Handle player death
                    OnDeath?.Invoke();
                    gm.GameOver();
                }

                if (doInvincibility)
                {
                    isInvincible = true;
                }
            }
        }
    }

    protected void Bounce(bool moving)
    {
        if (moving)
        {
            bounceTime += Time.deltaTime;
            if (bounceTime > bounceInterval)
            {
                body.transform.localPosition = new Vector3(body.transform.localPosition.x, 0, body.transform.localPosition.z);
                bounceTime = 0;
            }
            else
            {
                body.transform.localPosition = new Vector3(body.transform.localPosition.x, Mathf.Abs(Mathf.Sin((bounceTime / bounceInterval) * Mathf.PI)) * bounceHeight, body.transform.localPosition.z);
            }
        }
        else if (bounceTime != 0)
        {
            bounceTime += Time.deltaTime;
            if (bounceTime > bounceInterval)
            {
                body.transform.localPosition = new Vector3(body.transform.localPosition.x, 0, body.transform.localPosition.z);
                bounceTime = 0;
            }
            else
            {
                body.transform.localPosition = new Vector3(body.transform.localPosition.x, Mathf.Abs(Mathf.Sin((bounceTime / bounceInterval) * Mathf.PI)) * bounceHeight, body.transform.localPosition.z);
            }
        }
    }

    public void SetRectCooldown(float cooldown)
    {
        rectPreview.Cooldown = cooldown;
    }

    public void ResetRectCooldown()
    {
        if (selectedCharacter != null)
            rectPreview.Cooldown = selectedCharacter.PlatformCooldown;
        else
            rectPreview.Cooldown = 0.7f;
    }

    /*protected void RotateTowardsVelocity()
    {
        if (rb.velocity.sqrMagnitude > 0)
        {
            Vector3 vel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            Quaternion desiredRotation = Quaternion.LookRotation(vel);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }
    }*/
}
