using UnityEngine;
using System.Collections;

public class TributeController : MonoBehaviour
{

    public Vector3 mazeCenter;
    public float mazeRadius;
    public LayerMask minotaurCheckMask, theseusCheckMask;

    GameController gc;
    NavMeshAgent agent;

    bool fleeing;
    bool foundHero;

    public float wanderSpeed, chaseSpeed;

    void Start()
    {


        gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();


        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.SetDestination(Util.RandomNavMeshPt(mazeCenter, mazeRadius));
        fleeing = false;
        foundHero = false;
    }

    void Update()
    {
        //check to see if we're just going to get caught
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
                if (Physics.Raycast(transform.position, minotaurDir, out hitInfo, minotaurCheckMask.value))
                {
                    if (hitInfo.collider.GetComponent<PlayerController>())
                    {
                        //run away
                        Vector3 fleeDir = -minotaurDir;
                        Vector3 desired = transform.position + fleeDir*2;
                        agent.SetDestination(Util.GetNearestPtOnNavMesh(desired));
                        fleeing = true;
                        foundHero = false;
                    }
                }
                break;
            }

        }

        //if we're not fleeing maybe we can see theseus
        if(!fleeing)
        {
            colliders = Physics.OverlapSphere(transform.position, 10);
            foreach (Collider col in colliders)
            {
                HeroController theseus;
                if (theseus = col.GetComponent<HeroController>())
                {
                    Vector3 pos = theseus.transform.position;
                    Vector3 dir = (pos - transform.position).normalized;
                    //if we can also see the minotaur
                    RaycastHit hitInfo;
                    Debug.DrawRay(transform.position, dir);
                    if (Physics.Raycast(transform.position, dir, out hitInfo, theseusCheckMask.value))
                    {
                        if (hitInfo.collider.GetComponent<HeroController>())
                        {
                            agent.SetDestination(theseus.transform.position);
                            foundHero = true;
                        }
                    }
                    else if(foundHero)
                    {
                        agent.SetDestination(Util.RandomNavMeshPt(mazeCenter, mazeRadius));
                        foundHero = false;
                    }
                    break;
                }

            }
        }

        if (!agent.pathPending && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) 
            && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(Util.RandomNavMeshPt(mazeCenter, mazeRadius));
            fleeing = false;
            foundHero = false;
        }
        
        if(fleeing || foundHero)
        {
            agent.speed = chaseSpeed;
        }
        else
        {
            agent.speed = wanderSpeed;
        }
    }



    

}
