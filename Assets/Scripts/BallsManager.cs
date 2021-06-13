using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager: MonoBehaviour
{
    bool isAttached = false;
    bool canClamp = false;

    Rigidbody2D swingingBall;
    Rigidbody2D freezedBall;
    SpringJoint2D swingingJoint;
    SpringJoint2D freezedJoint;

    public Rigidbody2D ball1;
    public Rigidbody2D ball2;

    public float maxVelocity;
    public float maxForce;
    public float minDistance;
    public float maxDistance;

    private float currentVelocity;
    private float currentForce;

    // Start is called before the first frame update
    void Start()
    {
        swingingBall = ball1;
        swingingBall.tag = "SwingingBall";
        freezedBall = ball2;
        freezedBall.tag = "FreezedBall";
        StartAttachment();
    }

    void StartAttachment()
    {
        swingingJoint = swingingBall.GetComponent<SpringJoint2D>();
        swingingJoint.enabled = true;

        freezedJoint = freezedBall.GetComponent<SpringJoint2D>();
        freezedJoint.enabled = false;

        freezedBall.constraints = RigidbodyConstraints2D.FreezeAll;

        isAttached = true;
    }

    void StopAttachment()
    {
        freezedBall.constraints = RigidbodyConstraints2D.None;
        freezedBall.constraints = RigidbodyConstraints2D.FreezeRotation;
        SwapOpportunities();

        isAttached = false;
    }

    void SwapOpportunities()
    {
        Rigidbody2D exchanger = freezedBall;
        freezedBall = swingingBall;
        freezedBall.tag = "FreezedBall";
        swingingBall = exchanger;
        swingingBall.tag = "SwingingBall";
        swingingBall.AddForce((freezedBall.position - swingingBall.position).normalized * 20);
    }

    public void CanClamp(bool _canClamp)
    {
        canClamp = _canClamp;
    }

    public Vector3 GetBallsCenter()
    {
        return freezedBall.position;
    }

    /*
     // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isAttached == false && canClamp)
                StartAttachment();
            else if (isAttached == true && canClamp)
            {
                StopAttachment();
                StartAttachment();
            }
        }

        /*Vector2 frozen = freezedBall.transform.position;
        Vector2 swinging = swingingBall.transform.position;

        //Frozen angle
        frozen.x = frozen.x - swinging.x;
        frozen.y = frozen.y - swinging.y;
        float frozenAngle = Mathf.Atan2(frozen.y, frozen.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, frozenAngle));

        //Swinging angle
        swinging.x = swinging.x - frozen.x;
        swinging.y = swinging.y - frozen.y;
        float swingingAngle = Mathf.Atan2(swinging.y, swinging.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, swingingAngle));*/
        /*
    Vector2 forceDirection = -Vector2.Perpendicular(freezedBall.transform.position - swingingBall.transform.position);

        if (isAttached == true)
        {
            if ((swingingJoint.connectedAnchor - (Vector2) swingingJoint.transform.position).magnitude > swingingJoint.distance - 0.4f)
            {
                //swingingBall.AddForce(new Vector2(Input.GetAxis("Horizontal") * force, 0), ForceMode2D.Force);
                swingingBall.AddForce(forceDirection* (Input.GetAxis("Horizontal") * force), ForceMode2D.Force);
                if (swingingBall.velocity.magnitude > currentVelocity)
                    swingingBall.velocity = swingingBall.velocity.normalized* currentVelocity;
}

swingingJoint.distance -= Input.GetAxis("Vertical") / 40;
            swingingJoint.distance = Mathf.Max(swingingJoint.distance, minDistance);
            swingingJoint.distance = Mathf.Min(swingingJoint.distance, maxDistance);

            currentVelocity = maxVelocity / (maxDistance - swingingJoint.distance + 1);
            currentForce = maxForce / (maxDistance - swingingJoint.distance + 1);
            

            //Debug.Log(swingingJoint.distance);
            Debug.Log("vel: " + swingingBall.velocity.magnitude + " max vel: " + currentVelocity);
        }
    }
     */

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isAttached == false && canClamp)
                StartAttachment();
            else if (isAttached == true && canClamp)
            {
                StopAttachment();
                StartAttachment();
            }
        }

        if (isAttached == true)
        {
            if ((swingingJoint.connectedAnchor - (Vector2)swingingJoint.transform.position).magnitude > swingingJoint.distance - 0.4f)
            {
                swingingBall.AddForce(new Vector2(Input.GetAxis("Horizontal") * currentForce, 0), ForceMode2D.Force);
                if (swingingBall.velocity.magnitude > currentVelocity)
                    swingingBall.velocity = swingingBall.velocity.normalized * currentVelocity;
            }
            
            swingingJoint.distance -= Input.GetAxis("Vertical") / 40;
            swingingJoint.distance = Mathf.Max(swingingJoint.distance, minDistance);
            swingingJoint.distance = Mathf.Min(swingingJoint.distance, maxDistance);

            currentVelocity = maxVelocity / (maxDistance - swingingJoint.distance + 1);
            currentForce = maxForce / (maxDistance - swingingJoint.distance + 1);
            //Debug.Log(swingingJoint.distance);
            Debug.Log("vel: " + swingingBall.velocity.magnitude + " max vel: " + currentVelocity + " cur force: " + currentForce);
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "")
        {
            //ToggleCanClamp(true);
            //gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }*/
}
