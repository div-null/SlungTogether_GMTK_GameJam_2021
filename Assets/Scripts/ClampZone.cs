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
        if (other.tag == "SwingingBall")
        {
            //ToggleCanClamp(true);
            BallsManager b = other.GetComponentInParent<BallsManager>();
            ToggleCanClamp(ref b, true);
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (other.tag == "FreezedBall")
        {
            BallsManager b = other.GetComponentInParent<BallsManager>();
            if (b.IsLoose())
            {
                b.SwapOpportunities();
                //b.StartAttachment();
                ToggleCanClamp(ref b, true);
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    private void ToggleCanClamp(bool canClamp)
    {
        FindObjectOfType<BallsManager>().CanClamp(canClamp);
    }

    private void ToggleCanClamp(ref BallsManager manager, bool canClamp)
    {
        manager.CanClamp(canClamp);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "SwingingBall" || (collision.tag == "FreezedBall" && collision.GetComponentInParent<BallsManager>().IsLoose()))
        {
            ToggleCanClamp(true);
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "SwingingBall" || (other.tag == "FreezedBall" && other.GetComponentInParent<BallsManager>().IsLoose()))
        {
            ToggleCanClamp(false);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
