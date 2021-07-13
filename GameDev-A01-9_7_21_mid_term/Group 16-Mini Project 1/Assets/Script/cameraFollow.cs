using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    Transform queenTransform;
    float leftBarrier = -55f;
    float rightBarrier = 55f;
    float upBarrier = 100f;
    float downBarrier = -10f;

    void Start()
    {
        queenTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 curPosition = transform.position;
        //curPosition.x = leftBarrier;
        //curPosition.y = 3f;
        transform.position = curPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 curPosition = transform.position;
        if (queenTransform.position.x < leftBarrier)
            curPosition.x = leftBarrier;
        else if (queenTransform.position.x > rightBarrier)
            curPosition.x = rightBarrier;
        else curPosition.x = queenTransform.position.x;

        if (queenTransform.position.y > upBarrier)
            curPosition.y = upBarrier;
        else if (queenTransform.position.y < downBarrier)
            curPosition.y = downBarrier;
        else curPosition.y = queenTransform.position.y;

        transform.position = curPosition;
    }
}
