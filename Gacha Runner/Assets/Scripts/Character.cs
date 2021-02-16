using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Rigidbody2D rb;
    [SerializeField]
    protected GameObject body;

    //Other values
    protected bool grounded = false;
    protected float bounceTime = 0.0f;
    protected float gravity = 5.0f;

    //Config values
    [SerializeField]
    protected float speed = 10f;
    protected float bounceHeight = 0.25f;
    protected float bounceInterval = 0.3f;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        MoveForward();
    }

    protected void MoveForward()
    {
        Vector2 force = transform.right * speed;
        force += Vector2.down * gravity;
        rb.velocity = force;
        Bounce(true);
    }

    /*protected void MoveTowardsPoint(Vector3 point)
    {
        point = new Vector3(point.x, transform.position.y, point.z);
        Vector3 dir = point - transform.position;
        dir.Normalize();
        cc.Move(dir * speed);
    }*/

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
