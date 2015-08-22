using UnityEngine;
using System.Collections.Generic;

public class TributeSpawner : MonoBehaviour
{

    public GameObject tributePrefab;
    public int numTributes;
    public NavGrid navGrid;
    public List<NavPoint> notAllowedPoints;

    void Start()
    {
        for(int i = 0; i < numTributes; i++)
        {
            NavPoint startPoint = GetRandomNavPoint(navGrid);
            GameObject trib = (GameObject)Instantiate(tributePrefab, 
                startPoint.position, Quaternion.identity);
            //fix the y position
            trib.transform.position = new Vector3(trib.transform.position.x,
                tributePrefab.transform.position.y,
                trib.transform.position.z);
        }
    }

    NavPoint GetRandomNavPoint(NavGrid grid)
    {
        NavPoint point = null;
        do
        {
            point = grid.points[Random.Range(0, grid.points.Count)];
        } while (notAllowedPoints.Contains(point));
        return point;
    }

}
