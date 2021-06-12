using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    Rigidbody2D rigidBody;
    DistanceJoint2D distanceJoint;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        distanceJoint = this.GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((distanceJoint.connectedAnchor - (Vector2)gameObject.transform.position).magnitude > distanceJoint.distance - 0.1f)
        rigidBody.AddForce(new Vector2(Input.GetAxis("Horizontal") * 1, 0), ForceMode2D.Force);
        //distanceJoint.anchor = new Vector2(Input.GetAxis("Horizontal") / 10, 0);
    }
}
