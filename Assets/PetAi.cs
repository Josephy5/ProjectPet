using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AI;
using UnityEngine;

public class PetAi : MonoBehaviour
{
    [Space(10)]
    #region GameOBJ Var
    [Header("GameObject Reference")]
    [Tooltip("Reference of the navmesh")]
    public NavMeshAgent agent;
    [Tooltip("Reference of the layers")]
    //public LayerMask isGround, isPlayer, isObstacle;
    public LayerMask isGround;
    //[Tooltip("Reference of the animation object")]
    //private Animator anim;

    //public GameObject bullet;
    //public AudioSource SFX;
    private Transform pointOfReturn;
    #endregion

    #region Patroling Var
    [Header("Patroling Var")]
    //patroling
    [Tooltip("the cords of the destination")]
    public Vector3 walkpoint;
    bool walkPointSet;
    [Tooltip("how far will the enemy walk, NOTE: enemy may still walk outside of specified walk range when first spawned")]
    public float walkPointRange;
    [Tooltip("how fast will the ai walk (when the player is not detected)")]
    public float desiredWalkSpeed = 3.5f;
    //private Vector3 offset;
    private int check;
    #endregion

    #region States Var
    [Header("States Var")]
    //States
    [Tooltip("How far can the enemy see")]
    public float sightRange;
    //[Tooltip("Enemy bool states")]
    //public bool playerInSightRange, playerInAttackRange;
    //[Tooltip("will the ai track or attack the player")]
    //public bool willTrackPlayer, willAttackPlayer;
    //[Tooltip("will the ai place something")]
    //public bool willPlaceObstacle;
    //private bool hasShouted;
    private bool hasStopped;
    #endregion

    IEnumerator Patroling()
    {
        if (hasStopped == false)
        {
            agent.speed = desiredWalkSpeed;

            //if walkpoint is not set
            if (!walkPointSet)
            {
                SearchWalkPoint();
                //horrible solution to prevent the ai from being stuck at one place
                if (check > 1000)
                {
                    stopPatrolling();
                    float randomZ = Random.Range(-30, 30);
                    float randomX = Random.Range(-30, 30);
                    walkpoint = new Vector3(pointOfReturn.position.x + randomX, pointOfReturn.position.y, pointOfReturn.position.z + randomZ);
                    walkPointSet = true;
                    check = 0;
                }
            }

            //if walkpoint has been set
            if (walkPointSet)
            {
                agent.isStopped = false;
                agent.SetDestination(walkpoint);

            }

            Vector3 distanceToWalkPoint = transform.position - walkpoint;
            //print(distanceToWalkPoint.magnitude);
            //to-do add a pause for the ai whenever they reached thier destination (not a long pause, just a short one). If you don't want a pause, we can forget about it
            //des reached
            Debug.Log("MAG:"+distanceToWalkPoint.magnitude);
            if (distanceToWalkPoint.magnitude < 0.1f)
            {
                hasStopped = true;
                walkPointSet = false;
            }

        }
        else if (hasStopped == true)
        {
            yield return new WaitForSeconds(2.5f);
            hasStopped = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //if (walkingOnTerrain) walkpoint = walkpoint + offset;
        check += 1;
        //checks if waypoint is suitable
        if (Physics.Raycast(walkpoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //offset = new Vector3(0, 1, 0);
        //isDead = false;
        pointOfReturn = GameObject.Find("AI_returnHere").transform;

        //get animator component
        //anim = GetComponent<Animator>();
    }
    private void stopPatrolling()
    {
        agent.isStopped = true;
    }

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        StartCoroutine(Patroling());
    }
}
