using UnityEngine;
using System.Collections;

public class TributeController : MonoBehaviour
{

    public Pathfinder pathfinder;
    public NavPoint startPoint;
    public NavGrid grid;

    public NavPoint nextPoint, lastPoint;


    public PointMovementController mover;
    GameController gc;

    void Start()
    {
        pathfinder = GameObject.FindWithTag("Pathfinder").GetComponent<Pathfinder>();
        grid = GameObject.FindWithTag("NavGrid").GetComponent<NavGrid>();

        lastPoint = startPoint;
        nextPoint = lastPoint.GetRandomNeighbour();

        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        mover.OnArrival += SelectNewDest;
        mover.SetNewDestination(lastPoint.position, nextPoint.position);
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

    }

    void SelectNewDest()
    {
        NavPoint oldNext = nextPoint;
        nextPoint = nextPoint.TryGetRandomDifferentPoint(lastPoint);
        lastPoint = oldNext;
        mover.SetNewDestination(lastPoint.position, nextPoint.position);
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
