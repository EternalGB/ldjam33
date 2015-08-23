using UnityEngine;
using System.Collections;

public class PointMovementController : MonoBehaviour
{

    public float speed;
    Vector3 lastPos, nextPos;

    float lerpSpeed, lerpTimer;
    bool moving = false;

    public delegate void Arrived();
    public event Arrived OnArrival;

    PointMovementController backup;
    public bool isBackup = false;

    void Start()
    {
        if(!isBackup)
        {
            backup = gameObject.AddComponent<PointMovementController>();
            backup.isBackup = true;
            backup.speed = speed;
            backup.OnArrival += StartMoving;
        }
        
    }

    public void SetNewDestination(Vector3 currentPosition, Vector3 destination)
    {
        lastPos = currentPosition;
        nextPos = destination;
        //check to see if we're actually at current position
        //or if we have to move a bit closer
        if(Vector3.Distance(transform.position, currentPosition) < 0.2f)
        {
            //if we're really close just jump there
            transform.position = currentPosition;
            StartMoving();
        }
        else
        {
            //otherwise we have to lerp closer
            backup.SetNewDestination(transform.position, currentPosition);
        }
        

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
}
