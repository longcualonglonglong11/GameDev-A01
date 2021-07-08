using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    Transform queenTransform;
    float leftBarrier = -4;
    float rightBarrier = 10.0f;
    void Start()
    {
        queenTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 curPosition = transform.position;
        curPosition.x = leftBarrier;
        curPosition.y = 3f;
        transform.position = curPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 curPosition = transform.position;
        curPosition.x = queenTransform.position.x;
        curPosition.y = queenTransform.position.y;
        transform.position = curPosition;
    }
}
