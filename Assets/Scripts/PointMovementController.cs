﻿using UnityEngine;
using System.Collections;

public class PointMovementController : MonoBehaviour
{

    public float speed;
    Vector3 lastPos, nextPos;

    float lerpSpeed, lerpTimer;
    bool moving = false;

    public delegate void Arrived();
    public event Arrived OnArrival;



    public void SetNewDestination(Vector3 currentPosition, Vector3 destination)
    {
        lastPos = currentPosition;
        nextPos = destination;
        
        StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            if(lerpTimer >= 1)
            {
                if(OnArrival != null)
                {
                    OnArrival();
                }
            }
            else
            {
                lerpTimer += lerpSpeed * Time.deltaTime;
                transform.position = Vector3.Lerp(lastPos, nextPos, lerpTimer);
            }
        }
    }

    void StartMoving()
    {
        lerpSpeed = speed / Vector3.Distance(lastPos, nextPos);
        lerpTimer = 0;
        moving = true;
    }

    public void Resume()
    {
        lastPos = transform.position;
        StartMoving();
    }

    public void Stop()
    {
        moving = false;
    }

    public Vector3 GetDir()
    {
        return (nextPos - lastPos).normalized;
    }
}
