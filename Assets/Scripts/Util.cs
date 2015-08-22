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
  
}
