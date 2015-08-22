using UnityEngine;
using System.Collections;

public class TributeController : MonoBehaviour
{

    public Pathfinder pathfinder;
    public NavPoint startPoint;
    public NavGrid grid;

    public float speed;
    public Vector3 dir;
    public NavPoint[] currentPath;
    public int pathIndex;

    void Start()
    {
        pathfinder = GameObject.FindWithTag("Pathfinder").GetComponent<Pathfinder>();
        grid = GameObject.FindWithTag("NavGrid").GetComponent<NavGrid>();

        NavPoint nextPoint = GetRandomNavPoint(startPoint);
        currentPath = pathfinder.GetShortestPath(startPoint, nextPoint);
        pathIndex = 0;
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
        if (currentPath != null)
        {
            
            if (Vector3.Distance(transform.position, currentPath[pathIndex].position) < 0.4f)
            {
                pathIndex++;
                if (pathIndex >= currentPath.Length)
                {
                    NavPoint nextGoal = GetRandomNavPoint(currentPath[currentPath.Length - 1]);
                    if (nextGoal != null)
                    {
                        currentPath = pathfinder.GetShortestPath(currentPath[currentPath.Length - 1], nextGoal);
                        pathIndex = 0;
                    }
                    else
                    {
                        currentPath = null;
                    }
                }
            }
            if (pathIndex - 1 >= 0 && pathIndex < currentPath.Length)
                dir = (currentPath[pathIndex].position - currentPath[pathIndex - 1].position).normalized;
        }
        else
        {
            dir = Vector3.zero;
        }

        transform.position += dir * speed * Time.deltaTime;
    }

}
