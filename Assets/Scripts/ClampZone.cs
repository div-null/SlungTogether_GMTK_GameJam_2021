using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampZone : MonoBehaviour
{
    Collider2D Collider2D;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        print("tag: " + other.tag);
        if (other.tag == "Player")
        {
            ToggleCanClamp(other.GetComponent<BallPhysics>(), true);
        }
    }

    private void ToggleCanClamp(BallPhysics ball, bool canClamp)
    {
        ball.CanClamp(canClamp);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ToggleCanClamp(other.GetComponent<BallPhysics>(), false);
        }
    }
}
