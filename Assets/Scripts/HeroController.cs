using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{

    DFS dfs;
    public StartPoint startPoint;
    NavPoint firstPoint, nextPoint;
    GameController gc;

    NavMeshAgent agent;

    public LayerMask tributeCheckMask;
    bool foundTribute;
    public float chaseSpeed, searchSpeed;

    bool escaping = false;

    public AudioClipPlayer footsteps;
    float stepTimer = 0;
    float stepInterval;

    bool hitMinotaur = false;

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
        foundTribute = false;
    }

    // Update is called once per frame
    void Update()
    {
        stepInterval = 1.5f / agent.velocity.magnitude;
        //footsteps
        if(agent.velocity.magnitude > 0)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                stepTimer = 0;
                footsteps.PlayRandom();
            }
        }

        //check to see if we've hit the minotaur
        Collider[] colliders = Physics.OverlapSphere(transform.position, 
            transform.lossyScale.z, 1 << LayerMask.NameToLayer("Minotaur"));
        if(colliders != null && colliders.Length > 0)
        {
            PlayerController pc = colliders[0].GetComponent<PlayerController>();
            bool win = gc.MinotaurHasEnough();
            string msg = win ? "Theseus Defeated" : "You were not strong enough to beat Theseus. Collect more tributes";
            if(!hitMinotaur)
            {
                gc.GameOver(win, msg);
                hitMinotaur = true;
                Destroy(gameObject);
            }
            
        }

        //return to the start
        if(gc.TheseusAllCollected() && !escaping)
        {
            escaping = true;
            SetDestination(firstPoint.position);
        }


        //go to tribute
        colliders = Physics.OverlapSphere(transform.position, 8, 1<<LayerMask.NameToLayer("Tribute"));
        Collider target = Util.GetClosestMatching(colliders, transform.position,
            (col) =>
            {
                return col.GetComponent<TributeController>() != null;
            });
        if(target != null)
        {
            TributeController tribute = target.GetComponent<TributeController>();
            Vector3 pos = tribute.transform.position;
            Vector3 dir = (pos - transform.position).normalized;
            //if we can also see the minotaur
            RaycastHit hitInfo;
            Debug.DrawLine(transform.position, pos);
            if (Physics.Raycast(transform.position, dir, out hitInfo, tributeCheckMask.value))
            {
                if (hitInfo.collider.GetComponent<TributeController>())
                {
                    SetDestination(tribute.transform.position);
                    foundTribute = true;
                }
            }
            else if (foundTribute)
            {
                SetDestination(nextPoint.position);
                foundTribute = false;
            }
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            if(escaping)
            {
                gc.GameOver(false, "Theseus Escaped");
            }
            nextPoint = dfs.GetNextNavPoint();
            SetDestination(nextPoint.position);
            foundTribute = false;
        }

        if(foundTribute)
        {
            agent.speed = chaseSpeed;
        }
        else
        {
            agent.speed = searchSpeed;
        }
    }


    void SetDestination(Vector3 desired)
    {
        agent.SetDestination(Util.GetNearestPtOnNavMesh(desired));
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
