using UnityEngine;
using System.Collections;

public static class Util 
{

    public static float XZDistance(Vector3 p1, Vector3 p2)
    {
        p1.y = 0;
        p2.y = 0;
        return Vector3.Distance(p1, p2);
    }
  
    public static bool AreXZCollinear(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return p1.x * (p2.y - p3.y) + p2.x * (p3.y - p1.y) + p3.x * (p1.y - p2.y) == 0;
    }

    public static Vector3 RandomNavMeshPt(Vector3 mazeCenter, float mazeRadius)
    {
        return GetNearestPtOnNavMesh(RandomPointInXZArea(mazeCenter, mazeRadius));
    }

    public static Vector3 RandomPointInXZArea(Vector3 center, float radius)
    {
        return center + new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
    }

    public static bool PointOnNavMesh(Vector3 desiredPt, float searchRadius, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(desiredPt, out hit, searchRadius, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public static Vector3 GetNearestPtOnNavMesh(Vector3 desiredDest)
    {
        Vector3 dest;
        while (!Util.PointOnNavMesh(desiredDest, 5, out dest)) ;
        return dest;
    }

    public static Collider GetClosestMatching(Collider[] colliders, Vector3 position, System.Predicate<Collider> check)
    {
        float bestDist = float.MaxValue;
        Collider bestCol = null;
        foreach(Collider col in colliders)
        {
            if(check(col))
            {
                float dist = Vector3.Distance(col.transform.position, position);
                if(dist < bestDist)
                {
                    bestDist = dist;
                    bestCol = col;
                }
            }
        }
        return bestCol;
    }
}
