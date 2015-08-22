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
}
