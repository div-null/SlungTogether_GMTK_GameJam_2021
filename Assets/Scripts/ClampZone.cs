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
        if (other.tag == "FreezedBall")
        {
            ToggleCanClamp(true);
        }
    }

    private void ToggleCanClamp(bool canClamp)
    {
        FindObjectOfType<BallsControls>().CanClamp(canClamp);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "FreezedBall")
        {
            ToggleCanClamp(false);
        }
    }
}
