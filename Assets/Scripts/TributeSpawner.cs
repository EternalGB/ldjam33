using UnityEngine;
using System.Collections.Generic;

public class TributeSpawner : MonoBehaviour
{

    public GameObject tributePrefab;
    public NavGrid navGrid;
    public List<NavPoint> notAllowedPoints;

    public void SpawnTributes(int numTributes)
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
            //trib.GetComponent<TributeController>().startPoint = startPoint;
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
