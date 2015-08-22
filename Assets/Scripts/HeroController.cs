﻿using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{

    DFS dfs;
    public StartPoint startPoint;
    public float speed;
    public Vector3 dir;
    public NavPoint[] currentPath;
    int pathIndex;
    public Pathfinder pathfinder;
    NavPoint firstPoint;

    // Use this for initialization
    void Start()
    {
        firstPoint = startPoint.startingNavPoint;
        dfs = new DFS(firstPoint);
        NavPoint nextPoint = dfs.GetNextNavPoint();
        //put us at the start point if we're not
        if (Vector3.Distance(transform.position, nextPoint.position) > 0.25f)
            transform.position = nextPoint.position;
        nextPoint = dfs.GetNextNavPoint();
        currentPath = pathfinder.GetShortestPath(firstPoint, nextPoint);
        pathIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPath != null)
        {
            if (Vector3.Distance(transform.position, currentPath[pathIndex].position) < 0.25f)
            {
                pathIndex++;
                if (pathIndex >= currentPath.Length)
                {
                    NavPoint nextGoal = dfs.GetNextNavPoint();
                    if(nextGoal != null)
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
            if(pathIndex - 1 >= 0)
                dir = (currentPath[pathIndex].position - currentPath[pathIndex-1].position).normalized;
        }
        else
        {
            dir = Vector3.zero;
        }

        transform.position += dir*speed * Time.deltaTime;
    }
}