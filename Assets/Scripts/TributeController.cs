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
    int pathIndex;
    NavPoint firstPoint;

    void Start()
    {
        pathfinder = GameObject.FindWithTag("Pathfinder").GetComponent<Pathfinder>();
        grid = GameObject.FindWithTag("NavGrid").GetComponent<NavGrid>();

        NavPoint nextPoint = GetRandomNavPoint(grid);
        Debug.Log("Init tribute start point null? " + (startPoint == null));
        currentPath = pathfinder.GetShortestPath(startPoint, nextPoint);
        pathIndex = 0;
    }

    NavPoint GetRandomNavPoint(NavGrid grid)
    {
        return grid.points[Random.Range(0, grid.points.Count)];
    }

    void Update()
    {
        if (currentPath != null)
        {
            if (Vector3.Distance(transform.position, currentPath[pathIndex].position) < 0.25f)
            {
                pathIndex++;
                if (pathIndex >= currentPath.Length)
                {
                    NavPoint nextGoal = GetRandomNavPoint(grid);
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
            if (pathIndex - 1 >= 0)
                dir = (currentPath[pathIndex].position - currentPath[pathIndex - 1].position).normalized;
        }
        else
        {
            dir = Vector3.zero;
        }

        transform.position += dir * speed * Time.deltaTime;
    }

}
