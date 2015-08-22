using UnityEngine;
using System.Collections.Generic;

public class NavGrid : MonoBehaviour
{

	public List<NavPoint> points;

    public LayerMask pointMask, blockingMask;

	public void MakeSymmetrical()
	{
		foreach(NavPoint np in points) 
        {
			foreach(NavPoint neighbour in np.neighbours) 
            {
				if(!neighbour.neighbours.Contains(np))
					neighbour.neighbours.Add(np);
			}
		}
	}

    Vector3[] searchDirections = 
    {
        Vector3.right, 
        -Vector3.right, 
        Vector3.forward, 
        -Vector3.forward
    };

    public void FindNeighboursViaRaycast()
    {
        
        foreach(NavPoint point in points)
        {
            foreach(Vector3 dir in searchDirections)
            {
                Ray ray = new Ray(point.transform.position, dir);
                RaycastHit hitInfo;
                if(Physics.Raycast(ray, out hitInfo))
                {
                    NavPoint neighbour = hitInfo.collider.GetComponent<NavPoint>();
                    if(neighbour)
                    {
                        point.neighbours.Add(neighbour);
                    }
                }
            }
        }
    }

    public void ClearNeighbours()
    {
        foreach (NavPoint point in points)
        {
            point.neighbours.Clear();
        }
    }

    bool IsLayerInMask(int layer, LayerMask mask)
    {
        return (mask.value & 1 << layer) != 0;
    }

}

