using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWiggle : MonoBehaviour
{
    public float wiggleDistance = 0.1f;
    public float wiggleSpeed = 1f;
 

    private Vector3 startPos;
    void Start() {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float yPosition = Mathf.Sin(Time.time * wiggleSpeed) * wiggleDistance;
        transform.localPosition = new Vector3(startPos.x, startPos.y + yPosition, startPos.z);
    }
}
