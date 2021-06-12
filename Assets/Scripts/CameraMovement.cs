using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    BallsManager ballsManager;

    // Start is called before the first frame update
    void Start()
    {
        ballsManager = FindObjectOfType<BallsManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
