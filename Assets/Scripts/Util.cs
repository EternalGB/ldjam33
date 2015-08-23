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
        Vector3 randPt;
        Vector3 result;
        do
        {
            randPt = RandomPointInXZArea(mazeCenter, mazeRadius);
        } while (!PointOnNavMesh(randPt, 2, out result));
        return result;
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
}
