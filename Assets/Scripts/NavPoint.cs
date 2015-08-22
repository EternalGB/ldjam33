using UnityEngine;
using System.Collections.Generic;

public class NavPoint : MonoBehaviour
{

    public bool visited = false;

	public List<NavPoint> neighbours;
	public Vector3 position
	{
		get {return transform.position;}
		set {transform.position = value;}
	}

	public void RemoveDeadNeighbours()
	{
		List<NavPoint> nps = new List<NavPoint>();
		foreach(NavPoint np in neighbours)
			if(np != null)
				nps.Add(np);
		neighbours = nps;
        
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position,0.35f);
		if(neighbours != null) 
        {
			foreach(NavPoint t in neighbours) 
            {

				Gizmos.DrawLine(transform.position,t.transform.position);
			}
		}
	}

    public NavPoint GetRandomNeighbour()
    {
        if (neighbours.Count > 0)
        {
            return neighbours[Random.Range(0,neighbours.Count)];
        }
        else
            return null;
    }

    public NavPoint TryGetRandomDifferentPoint(NavPoint notThis)
    {
        if (neighbours.Count == 1)
            return neighbours[0];
        else if(neighbours.Count == 2)
        {
            return neighbours.Find( (np) => {
                return !np.Equals(notThis);
            });
        }
        else
        {
            NavPoint point;
            do
            {
                point = GetRandomNeighbour();
            } while (point.Equals(notThis));
            return point;
        }
    }

    public override bool Equals(object o)
    {
        NavPoint other = o as NavPoint;
        if (other == null)
            return false;
        return this.GetInstanceID() == other.GetInstanceID();
    }

}

