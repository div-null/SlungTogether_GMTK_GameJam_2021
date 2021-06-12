using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
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
    public float force;
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

        freezedBall.constraints = RigidbodyConstraints2D.FreezePosition;

        isAttached = true;
    }

    void StopAttachment()
    {
        freezedBall.constraints = RigidbodyConstraints2D.None;
        //freezedBall.constraints = RigidbodyConstraints2D.FreezeRotation;
        SwapOpportunities();

        isAttached = false;
    }

    //If the user presses Space when they are not ready to attach somewhere
    void DetachCompletely()
    {
        freezedBall.constraints = RigidbodyConstraints2D.None;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canClamp)
            {
                if (isAttached == false)
                    StartAttachment();
                else
                {
                    StopAttachment();
                    StartAttachment();
                }
            }
            else
            {
                DetachCompletely();
            }
            
        }

        Vector2 forceDirection = -Vector2.Perpendicular(freezedBall.transform.position - swingingBall.transform.position);

        if (isAttached == true)
        {
            float dist = (swingingJoint.connectedAnchor - (Vector2)swingingJoint.transform.position).magnitude;
            print("dist: " + dist + ", swingJoint dist: " + (swingingJoint.distance - 0.4f));
            currentVelocity = maxVelocity / (maxDistance - swingingJoint.distance + 1);
            currentForce = maxForce / (maxDistance - swingingJoint.distance + 1);
            if (dist > swingingJoint.distance - 0.4f) //why is this here?
            {
                //swingingBall.AddForce(forceDirection * (Input.GetAxis("Horizontal") * force), ForceMode2D.Force);
                swingingBall.AddForce(forceDirection * (Input.GetAxis("Horizontal") * currentForce), ForceMode2D.Force);
                if (swingingBall.velocity.magnitude > currentVelocity)
                    swingingBall.velocity = swingingBall.velocity.normalized * currentVelocity;
            }
            else
                print("Goofy stuff is happening");
            
            swingingJoint.distance -= Input.GetAxis("Vertical") / 40;
            swingingJoint.distance = Mathf.Max(swingingJoint.distance, minDistance);
            swingingJoint.distance = Mathf.Min(swingingJoint.distance, maxDistance);

            /*currentVelocity = maxVelocity / (maxDistance - swingingJoint.distance + 1);
            currentForce = maxForce / (maxDistance - swingingJoint.distance + 1);*/
            

            //Debug.Log(swingingJoint.distance);
            //Debug.Log("vel: " + swingingBall.velocity.magnitude + " max vel: " + currentVelocity);
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
