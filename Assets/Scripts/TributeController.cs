using UnityEngine;
using System.Collections;

public class TributeController : MonoBehaviour
{

    public Vector3 mazeCenter;
    public float mazeRadius;


    GameController gc;
    NavMeshAgent agent;

    bool fleeing;

    void Start()
    {


        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();


        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.SetDestination(RandomNavMeshPt());
        fleeing = false;
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.lossyScale.z);
        foreach(Collider col in colliders)
        {
            if(col.GetComponent<HeroController>())
            {
                gc.TheseusGet();
                Destroy(gameObject);
            }
            else if(col.GetComponent<PlayerController>())
            {
                gc.MinotaurGet();
                Destroy(gameObject);
            }
        }

        
        
        //check to see if the minotaur is nearby
        if(!fleeing)
        {
            colliders = Physics.OverlapSphere(transform.position, 8);
            foreach (Collider col in colliders)
            {
                PlayerController minotaur;
                if (minotaur = col.GetComponent<PlayerController>())
                {
                    Vector3 minotaurPos = minotaur.transform.position;
                    Vector3 minotaurDir = (minotaurPos - transform.position).normalized;
                    //if we can also see the minotaur
                    RaycastHit hitInfo;
                    Debug.DrawRay(transform.position, minotaurDir);
                    if (Physics.Raycast(transform.position, minotaurDir, out hitInfo))
                    {
                        if (hitInfo.collider.GetComponent<PlayerController>())
                        {
                            //run away
                            Vector3 dest;
                            Vector3 fleeDir = -minotaurDir;
                            Vector3 desired = transform.position + fleeDir*2;
                            while (!PointOnNavMesh(desired, 5, out dest)) ;
                            agent.SetDestination(dest);
                            fleeing = true;
                        }
                    }
                    break;
                }

            }
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            agent.SetDestination(RandomNavMeshPt());
            fleeing = false;
        }
        
         
    }

    Vector3 RandomNavMeshPt()
    {
        Vector3 randPt;
        Vector3 result;
        do
        {
            randPt = RandomPointInMaze(mazeCenter, mazeRadius);
        } while (!PointOnNavMesh(randPt, 2, out result));
        return result;
    }

    Vector3 RandomPointInMaze(Vector3 center, float radius)
    {
        return center + new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
    }

    bool PointOnNavMesh(Vector3 desiredPt, float searchRadius, out Vector3 result)
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
