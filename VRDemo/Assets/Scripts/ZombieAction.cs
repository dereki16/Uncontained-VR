using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAction : MonoBehaviour
{
    public ZombieSpawner zs;
    public AgentLinkMover link;
    public ObjectPooler op;
    public Animator anim;

    public Transform player;
    public GameObject[] window;
    public GameObject[] windowStop;
    public GameObject[] planks;
    public Transform[] windowTransform;
    public Transform[] windowStopTransform;
    public Transform[] navMeshTransform;
    private GameObject forward;
    public LayerMask whatIsGround, whatIsPlayer, whatIsGrabbable;

    public NavMeshAgent agent;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange, attackStillRange, grabbableRange, growlableRange;
    public bool playerInSightRange, playerInAttackRange, playerStillInAttackRange, grabbableInAttackRange, playerInGrowlableRange;
    public bool zombieDead;
    public bool attackBarricade;
    public bool canChase;
    public bool zombieInside;
    public bool outsideWindow;
    public int health = 10;
    
    // Timers
    private float deathTimer;
    private float settingDeathTimer;
    public float attackBarricadeTimer;
    public float attackBarricadeTimeAllotted;
    public float walkTimer;
    private float walkTimerAlloted;
    private float growlTimer;
    private float growlTimerAllotted;

    // Window
    public GameObject[] lookAt;
    public Transform[] lookAtTransform;
    public NavMeshScript nav;
    private GameObject windowBox;
    private GameObject[] navMesh;
    private bool reposition;
    public int counter = 0;
    public Vector3 windowStopDestination;

    // Sphere detecter
    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;
    private Vector3 origin;
    private Vector3 direction;
    private float currentHitDistance;

    // Planks
    public List<GameObject> plankList = new List<GameObject>();
    public bool plankInRange;

    // Speeds
    public float startingSpeed;
    public float attackingSpeed;

 


    // Stats
    public static int zombiesKilled;
    public Transform zombieHeight;
    public int healthyRun;
    public float animSpeed;
    private float agentSpeed;
    private int randGrowl;

    // Blood
    public GameObject blood;
    public GameObject bloodInst;
    public int bloodCounter;


    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        window = GameObject.FindGameObjectsWithTag("WindowBox");
        windowStop = GameObject.FindGameObjectsWithTag("WindowStopPoint");
        navMesh = GameObject.FindGameObjectsWithTag("NavMesh");
        forward = GameObject.FindGameObjectWithTag("Forward");
        lookAt = GameObject.FindGameObjectsWithTag("Lookat");
        startingSpeed = anim.speed;

        if (window != null)
        {
            windowTransform = new Transform[window.Length];
            for (int ii = 0; ii < window.Length; ii++)
            {
                windowTransform[ii] = window[ii].transform;
            }
        }

        if (windowStop != null)
        {
            windowStopTransform = new Transform[windowStop.Length];
            for (int ii = 0; ii < windowStop.Length; ii++)
            {
                windowStopTransform[ii] = windowStop[ii].transform;
            }
        }

        if (lookAt != null)
        {
            lookAtTransform = new Transform[lookAt.Length];
            for (int ii = 0; ii < lookAt.Length; ii++)
            {
                lookAtTransform[ii] = lookAt[ii].transform;
            }
        }

        agent = GetComponent<NavMeshAgent>();
        zombieDead = false;
        health = 10 + zs.health;
        anim.speed = anim.speed + zs.speedInc;
        settingDeathTimer = 5f;
        deathTimer = settingDeathTimer;
        agentSpeed = agent.speed;

        walkTimerAlloted = 0.5f;
        walkTimer = walkTimerAlloted;

        growlTimer = growlTimerAllotted;
        attackingSpeed = startingSpeed / 2f;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }

    private void Update()
    {
        animSpeed = anim.speed;
        // check for attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        grabbableInAttackRange = Physics.CheckSphere(transform.position, grabbableRange, whatIsGrabbable);
        playerInGrowlableRange = Physics.CheckSphere(transform.position, growlableRange, whatIsPlayer);
        playerStillInAttackRange = Physics.CheckSphere(transform.position, attackStillRange, whatIsPlayer);

        origin = transform.position;
        direction = transform.forward;
        RaycastHit hit;
        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            if (hit.transform.gameObject.CompareTag("Plank"))
            {
                plankList.Add(hit.transform.gameObject);
            }
        }
        if (!outsideWindow && window != null)
        {
            GoToWindow();
        }

        if (attackBarricade && !zombieDead)
        {
            if (nav != null)
            {
                switch (nav.plankList.Count)
                {
                    case 4:
                        attackBarricadeTimer = 9.4f;
                        break;
                    case 3:
                        attackBarricadeTimer = 7.05f;
                        break;
                    case 2:
                        attackBarricadeTimer = 4.7f;
                        break;
                    case 1:
                        attackBarricadeTimer = 2.35f;
                        break;
                    case 0:
                        attackBarricadeTimer = 0f;
                        break;
                    default:
                        break;
                }
            }
            
            if (attackBarricadeTimer > 0)
            {
                attackBarricadeTimer -= Time.deltaTime;
            }
            else
            {
                attackBarricade = false;
            }
        }
        if (health <= 0) zombieDead = true;

        if (zombieDead)
        {
            OnDeath();
            if (deathTimer > 0)
                deathTimer -= Time.deltaTime;
            else
            {
                deathTimer = settingDeathTimer;
                zombiesKilled++;
                Destroy(this.gameObject);
                zombieDead = false;
            }
        }
        else if (!zombieDead && outsideWindow)
        {
            // make it so player will attack barricade if stopped, else vault
            if (attackBarricade) AttackBarricade();
            else if (!attackBarricade)
            {
                if (walkTimer > 0)
                {
                    walkTimer -= Time.deltaTime;
                    WalkBackwards();
                }
                else
                {
                    anim.SetBool("isWalkingBackwards", false);
                    Vault();
                }
            }

            if (playerInSightRange)
            {
                if (playerStillInAttackRange)
                {
                    anim.speed = attackingSpeed;
                    if (playerInAttackRange) AttackPlayer();
                }
                else if (!playerStillInAttackRange)
                {
                    if (!outsideWindow) canChase = true;
                    if (canChase) ChasePlayer();
                }
            }
            if (grabbableInAttackRange)
            {
                if (nav != null)
                    counter = nav.plankList.Count;
            }
        }

        if (zombieInside)
        {
            attackBarricade = false;
            agent.speed = 0.05f;
            attackBarricadeTimer = 0f;
        }

        // Animation speeds
        if (anim.GetBool("isAttacking") == true)
        {
            anim.speed = attackingSpeed;
        }
        else if (anim.GetBool("isDying") == true || anim.GetBool("isVaulting") == true || anim.GetBool("isWalkingBackwards") == true)
        {
            anim.speed = 1f;
        }
        else
        {
            anim.speed = startingSpeed;
        }

        // Random growling noises every 2 to 30 seconds
        if (playerInGrowlableRange)
        {
            if (growlTimer > 0f)
                growlTimer -= Time.deltaTime;
            else
            {
                randGrowl = Random.Range(1, 28);
                FindObjectOfType<AudioManager>().Play("ZG" + randGrowl.ToString());

                if (growlTimer <= 0f)
                    growlTimer = Random.Range(2, 9);
            }
        }
        else if (!playerInGrowlableRange)
        {
            if (growlTimer > 0f)
                growlTimer -= Time.deltaTime;
            else
            {
                randGrowl = Random.Range(1, 28);
                FindObjectOfType<AudioManager>().Play("ZG" + randGrowl.ToString());

                if (growlTimer <= 0f)
                    growlTimer = Random.Range(5, 30);
            }
        }

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    Transform GetClosest(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        return bestTarget;
    }

    private void GoToWindow()
    {
        agent.SetDestination(GetClosest(windowTransform).position);
    }

    private void AttackBarricade()
    {
        agent.SetDestination(windowStopDestination);
        anim.SetBool("isAttacking", true);
        agent.speed = 2f;
        transform.LookAt(GetClosest(lookAtTransform).position);
    }

    private void WalkBackwards()
    {
        anim.SetBool("isWalkingBackwards", true);
    }

    private void Vault()
    {
        agent.speed = agentSpeed;
        if (link.vaulting)
        {
            anim.SetBool("isVaulting", true);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            anim.SetBool("isVaulting", false);
            canChase = true;
        }
    }
    
    private void ChasePlayer()
    {
        anim.SetBool("isAttacking", false);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        canChase = false;
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        anim.SetBool(("isAttacking"), true);
        anim.SetBool("isVaulting", false);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void ZombieHit()
    {
        anim.SetBool(("isHit"), true);
        agent.speed = 0;
    }

    private void OnDeath()
    {
        anim.SetBool(("isDying"), true);
        agent.speed = 0;
        this.gameObject.tag = "Untagged";
        if (bloodCounter < 1)
        {
            Bleed();
            bloodCounter++;
        }
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    private void Bleed()
    {
        bloodInst = Instantiate(blood);
        bloodInst.transform.position = zombieHeight.position;
        bloodInst.transform.rotation = this.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health -= 5;
            // produce blood
            Bleed();
        }

        if (other.gameObject.CompareTag("WindowStopPoint"))
        {
            outsideWindow = true;
            attackBarricade = true;

            // get this destination
            windowStopDestination = this.transform.position;
        }

        if (other.gameObject.CompareTag("Link"))
        {
            outsideWindow = true;
        }

        if (other.gameObject.CompareTag("Inside"))
        {
            outsideWindow = false;
            attackBarricade = false;
        }

        if (other.gameObject.CompareTag("NavMesh"))
        {
            nav = other.gameObject.GetComponent<NavMeshScript>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hammer") || collision.gameObject.CompareTag("Plank"))
        {
            ZombieHit();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Hammer") || !collision.gameObject.CompareTag("Plank"))
        {
            anim.SetBool(("isHit"), false);
        }
    }
}
