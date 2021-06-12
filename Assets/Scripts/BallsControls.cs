using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsControls : MonoBehaviour
{
    bool isAttached = false;

    Rigidbody2D swingingBall;
    Rigidbody2D freezedBall;
    DistanceJoint2D swingingJoint;
    DistanceJoint2D freezedJoint;

    public Rigidbody2D ball1;
    public Rigidbody2D ball2;

    // Start is called before the first frame update
    void Start()
    {
        swingingBall = ball1;
        freezedBall = ball2;
        StartAttachment();
    }

    void StartAttachment()
    {
        swingingJoint = swingingBall.GetComponent<DistanceJoint2D>();
        swingingJoint.enabled = true;

        freezedJoint = freezedBall.GetComponent<DistanceJoint2D>();
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
        swingingBall = exchanger;
    }

    //for camera (test)
    public Vector3 GetPositionOfFreezeBall()
    {
        return freezedBall.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isAttached == false)
                StartAttachment();
            else
                StopAttachment();
        }

        if (isAttached == true)
        {
            if ((swingingJoint.connectedAnchor - (Vector2)gameObject.transform.position).magnitude > swingingJoint.distance - 0.1f)
                swingingBall.AddForce(new Vector2(Input.GetAxis("Horizontal") * 3, 0), ForceMode2D.Force);
        }
    }
}
