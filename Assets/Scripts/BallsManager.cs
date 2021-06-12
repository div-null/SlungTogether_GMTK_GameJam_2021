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
        return Vector3.zero;
    }

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
                swingingBall.AddForce(new Vector2(Input.GetAxis("Horizontal") * 3f, 0), ForceMode2D.Force);
            }

            swingingJoint.distance -= Input.GetAxis("Vertical") / 100;
        }
    }
}
