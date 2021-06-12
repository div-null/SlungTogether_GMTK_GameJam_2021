using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = FindObjectOfType<BallsControls>().GetPositionOfFreezeBall() - new Vector3(0, 0, 1);
    }
}
