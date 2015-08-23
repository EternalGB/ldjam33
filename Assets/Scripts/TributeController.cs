using UnityEngine;
using System.Collections;

public class TributeController : MonoBehaviour
{

    public Pathfinder pathfinder;
    public NavPoint startPoint;
    public NavGrid grid;
    public float speed;
    public Vector3 dir;
    public NavPoint nextPoint, lastPoint;

    float lerpTimer, lerpSpeed;

    GameController gc;

    void Start()
    {
        pathfinder = GameObject.FindWithTag("Pathfinder").GetComponent<Pathfinder>();
        grid = GameObject.FindWithTag("NavGrid").GetComponent<NavGrid>();

        lastPoint = startPoint;
        nextPoint = lastPoint.GetRandomNeighbour();
        lerpTimer = 0;
        lerpSpeed = speed/Vector3.Distance(lastPoint.position, nextPoint.position);

        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    NavPoint GetRandomNavPoint(NavPoint currentPoint)
    {
        NavPoint point = null;
        do
        {
            point = grid.points[Random.Range(0, grid.points.Count)];
        } while (point.GetInstanceID() == currentPoint.GetInstanceID());
        return point;
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.lossyScale.z/2);
        foreach(Collider col in colliders)
        {
            if(col.GetComponent<HeroController>())
            {
                gc.TheseusGet();
                Destroy(gameObject);
            }
            else if(col.GetComponent<PlayerController>())
            {
                gc.MinotaurGet();
                Destroy(gameObject);
            }
        }
        //if we're close enough to our destination then go to the next point
        if (lerpTimer >= 1)
        {
            //shift ourselves to where the point actually is
            //transform.position = nextPoint.position;

            NavPoint oldNext = nextPoint;
            nextPoint = nextPoint.TryGetRandomDifferentPoint(lastPoint);
            lastPoint = oldNext;
            lerpTimer = 0;
            lerpSpeed = speed/Vector3.Distance(lastPoint.position, nextPoint.position);
        }
        if (nextPoint != null)
        {
            lerpTimer += lerpSpeed*Time.deltaTime;
            transform.position = Vector3.Lerp(lastPoint.position, nextPoint.position, lerpTimer);
        }
    }

    void SetXZPosition(Vector3 position)
    {
        transform.position = new Vector3(
            position.x,
            transform.position.y,
            position.z
            );
    }



    

}
