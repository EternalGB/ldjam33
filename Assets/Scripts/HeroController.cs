using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{

    DFS dfs;
    public StartPoint startPoint;
    NavPoint firstPoint, nextPoint;
    GameController gc;

    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        //cache this for later
        firstPoint = startPoint.startingNavPoint;
        dfs = new DFS(firstPoint);
        //pops the top off the stack which is just the first point
        nextPoint = dfs.GetNextNavPoint();
        //put us at the start point if we're not
        if (Vector3.Distance(transform.position, nextPoint.position) > 0.05f)
            transform.position = nextPoint.position;
        //find our actual next point
        nextPoint = dfs.GetNextNavPoint();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        SetDestination(nextPoint.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            nextPoint = dfs.GetNextNavPoint();
            agent.SetDestination(nextPoint.position);
        }

    }

    void SetDestination(Vector3 desiredDest)
    {
        Vector3 dest;
        while (!Util.PointOnNavMesh(desiredDest, 5, out dest)) ;
        agent.SetDestination(dest);
    }

    void SetXZPosition(Vector3 position)
    {
        transform.position = new Vector3(
            position.x,
            transform.position.y,
            position.z
            );
    }
}
