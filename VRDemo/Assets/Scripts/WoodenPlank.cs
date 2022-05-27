using UnityEngine;
using UnityEngine.SceneManagement;

public class WoodenPlank : MonoBehaviour
{
    public OnTouchFloor floor;
    public HandPresence hp;
    public bool hammeredIn;
    public bool startCountdown;
    public bool respawn;
    public bool inContact;

    public GameObject drop;
    private GameObject windowBox;
    public float timer;
    public float respawnTimer;
    private float timeAllotted;
    private float respawnTimeAllotted;
    private int counter;

    public bool grabbingOntoPlank;

    public RighthandTasks rt;
    public string scene;

    public void Awake()
    {
        scene = SceneManager.GetActiveScene().name;
    }
    void Start()
    {
        hp = FindObjectOfType<HandPresence>();
        floor = FindObjectOfType<OnTouchFloor>();
        if (scene == "Tutorial")
            rt = GameObject.FindGameObjectWithTag("RT").GetComponent<RighthandTasks>();

        timeAllotted = 5f;
        timer = timeAllotted;

        respawnTimeAllotted = 1f;
        respawnTimer = respawnTimeAllotted;
    }

    void Update()
    {
        Vector3 pos;
        pos.y = -10f;
        if (this.gameObject.transform.position.y <= pos.y)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (!hammeredIn)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                this.gameObject.SetActive(false);
                if (counter < 1 && drop != null)
                {
                    counter++;
                    respawn = true;
                }
                timer = timeAllotted;
                counter = 0;
                startCountdown = false;
            }
        }

        if (respawn)
        {
            if (respawnTimer > 0)
            {
                respawnTimer -= Time.deltaTime;
            }
            else
            {
                drop.SetActive(true);
                respawnTimer = respawnTimeAllotted;
                respawn = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Window"))
        {
            drop = other.gameObject;
            startCountdown = true;
        }

        // sort out window box and jump thing
        if (other.gameObject.CompareTag("WindowBox"))
        {
            windowBox = other.gameObject;
        }

        if (other.gameObject.CompareTag("Hammer"))
        {
            if (!hammeredIn)
            {
                // give points 
                PointsFromBullets.points += 5;
                PointsFromBullets.totalPoints += 5;
                if (rt != null)
                    rt.SetupDoor();
                hammeredIn = true;
            }
            FindObjectOfType<AudioManager>().Play("Hammering");
        }

        if (other.gameObject.CompareTag("Hand"))
        {
            grabbingOntoPlank = true;
        }
    }
}
