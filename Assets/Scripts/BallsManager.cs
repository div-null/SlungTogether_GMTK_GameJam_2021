using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class BallsManager : MonoBehaviour
{
    public enum swingingAnimationState
    {
        Hang = 0,
        ReachLeft,
        ReachRight
    }

    public enum scribbleAnimationState
    {
        none = 0,
        up,
        down
    }

    bool isAttached = false;
    bool loose = false;
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
    public float changingDistanceSpeed;

    private float currentVelocity;
    private float currentForce;

    //Animation
    private swingingAnimationState swingingState;
    private scribbleAnimationState scribbleState;

    SkeletonAnimation freezeSpiderSkeleton;
    SkeletonAnimation swingingSpiderSkeleton;

    Vector3 previousSpringDirection;

    // Start is called before the first frame update
    void Start()
    {
        swingingBall = ball1;
        swingingBall.tag = "SwingingBall";
        freezedBall = ball2;
        freezedBall.tag = "FreezedBall";
        StartAttachment();
        previousSpringDirection = (swingingBall.position - freezedBall.position).normalized;
    }

    public void StartAttachment()
    {
        //idk what is this - Ivan
        if (loose)
        {
            loose = false;

        }
        else
        {
            swingingJoint = swingingBall.GetComponent<SpringJoint2D>();
            swingingJoint.enabled = true;

            freezedJoint = freezedBall.GetComponent<SpringJoint2D>();
            freezedJoint.enabled = false;

            freezedBall.constraints = RigidbodyConstraints2D.FreezePosition;

            isAttached = true;


            //Animations
            freezeSpiderSkeleton = freezedBall.GetComponentInChildren<SkeletonAnimation>();
            swingingSpiderSkeleton = swingingBall.GetComponentInChildren<SkeletonAnimation>();
            //Spider attaches to the object
            freezeSpiderSkeleton.AnimationState.SetAnimation(0, "Landing", false);
            freezeSpiderSkeleton.AnimationState.AddAnimation(0, "Idle", true, 0);
            swingingBall.GetComponentInChildren<SkeletonAnimation>().AnimationState.SetAnimation(0, "Hang", true);
        }

    }

    void StopAttachment()
    {
        freezedBall.constraints = RigidbodyConstraints2D.None;
        SwapOpportunities();

        isAttached = false;
        loose = false;
    }

    //If the user presses Space when they are not ready to attach somewhere
    void DetachCompletely()
    {
        print("flying free!!!!");
        freezedBall.constraints = RigidbodyConstraints2D.None;
        isAttached = false;
        loose = true;

    }

    public void SwapOpportunities()
    {
        Rigidbody2D exchanger = freezedBall;
        freezedBall = swingingBall;
        freezedBall.tag = "FreezedBall";
        swingingBall = exchanger;
        swingingBall.tag = "SwingingBall";
        swingingBall.AddForce((freezedBall.position - swingingBall.position).normalized * 20); //not sure this line is necessary
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*
            if (loose)
            {
                SwapOpportunities();
            }
            if (canClamp)
            {
                if (isAttached == false)
                    StartAttachment();
                else
                {
                    StopAttachment();
                }
            }
            */
            //if they are falling down because they detach and freezeBall can attach then StartAttachment
            if (isAttached == false && canClamp == true)
            {
                StartAttachment();
            }
            // if they attached
            else
            {
                // if swingingball can attach they attach
                if (canClamp == true)
                {
                    StopAttachment();
                    StartAttachment();
                }
                // else they cant attach and they just StopAttachment
                else
                {
                    StopAttachment();
                }
            }

            //else
            //{
            //    DetachCompletely();
            //}
        }

        Vector2 forceDirection = -Vector2.Perpendicular(freezedBall.transform.position - swingingBall.transform.position);

        if (isAttached == true)
        {
            float dist = (swingingJoint.connectedAnchor - (Vector2)swingingJoint.transform.position).magnitude;
            var currentSpringDirection = (swingingBall.position - freezedBall.position).normalized;
            
            var rotation = Vector2.SignedAngle(previousSpringDirection, currentSpringDirection);

            //var lookAt = Quaternion.LookRotation(currentSpringDirection, Vector3.up); //new Quaternion();
            //Debug.Log(currentSpringDirection);
           
            //lookAt.SetLookRotation(currentSpringDirection, swingingSpiderSkeleton.transform);
            //swingingSpiderSkeleton.transform.rotation = Quaternion.EulerAngles(0, 0, lookAt.ToEulerAngles().z);//new Quaternion(0, 0, lookAt.ToEulerAngles().z, 0);

            currentVelocity = maxVelocity * swingingJoint.distance / (maxDistance - minDistance);
            currentForce = maxForce * swingingJoint.distance / (maxDistance - minDistance);

            if (Mathf.Abs(rotation) > 8.0f)
            {
                swingingBall.velocity = swingingBall.velocity.normalized * currentVelocity;
            }
            if (dist > swingingJoint.distance - 0.4f) //why is this here?
            {
                //swingingBall.AddForce(forceDirection * (Input.GetAxis("Horizontal") * force), ForceMode2D.Force);
                swingingBall.AddForce(forceDirection * (Input.GetAxis("Horizontal") * currentForce), ForceMode2D.Force);
            }

            swingingJoint.distance -= Input.GetAxis("Vertical") / 100 * changingDistanceSpeed;
            swingingJoint.distance = Mathf.Max(swingingJoint.distance, minDistance);
            swingingJoint.distance = Mathf.Min(swingingJoint.distance, maxDistance);
        }
    }

    private void FixedUpdate()
    {
        var currentSpringDirection = (swingingBall.position - freezedBall.position).normalized;
        var rotation = Vector2.SignedAngle(previousSpringDirection, currentSpringDirection);
        previousSpringDirection = currentSpringDirection;

        //Animations
        if (Input.GetAxis("Vertical") < 0 && scribbleState != scribbleAnimationState.down)
        {
            swingingSpiderSkeleton.AnimationState.SetAnimation(0, "ClimbDown", true);
            scribbleState = scribbleAnimationState.down;
        }
        else if (Input.GetAxis("Vertical") > 0 && scribbleState != scribbleAnimationState.up)
        {
            swingingSpiderSkeleton.AnimationState.SetAnimation(0, "ClimbUp", true);
            scribbleState = scribbleAnimationState.up;
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            if (rotation > 0 && swingingState != swingingAnimationState.ReachRight)
            {
                swingingSpiderSkeleton.AnimationState.SetAnimation(0, "ReachRight", true);
                swingingState = swingingAnimationState.ReachRight;
            }
            else if (rotation < 0 && swingingState != swingingAnimationState.ReachLeft)
            {
                swingingSpiderSkeleton.AnimationState.SetAnimation(0, "ReachLeft", true);
                swingingState = swingingAnimationState.ReachLeft;
            }
            else
            {
                    swingingSpiderSkeleton.AnimationState.SetAnimation(0, "Hang", true);
                    swingingState = swingingAnimationState.Hang;
            }
        }
    }

    public bool IsLoose()
    {
        return loose;
    }
}
