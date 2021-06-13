using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    [SerializeField]
    Transform spider1;
    [SerializeField]
    Transform spider2;
    float scaleX, scaleY;

    // Start is called before the first frame update
    void Start()
    {
        AdjustScale();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustScale();
        ReOrient();
    }

    private void AdjustScale()
    {
        transform.position = (spider1.position + spider2.position) / 2f;
        //transform.localPosition = new Vector2(spider1.localPosition.x + spider2.localPosition.x, spider1.localPosition.y + spider2.localPosition.y) / 2;
        //scaleX = Mathf.Abs(spider1.position.x - spider2.position.x);
        //scaleY = Mathf.Abs(spider1.position.y - spider2.position.y);
        //transform.localScale = (scaleX * Vector2.right) + (scaleY * Vector2.up);
        Vector3 scale = Vector3.one;
        scale.y = Vector3.Distance(spider1.position, spider2.position) * 3;
        transform.localScale = scale;
    }

    private void ReOrient()
    {
        float angle = Vector3.SignedAngle(spider1.up, (spider2.position - spider1.position).normalized, Vector3.forward);
        transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
