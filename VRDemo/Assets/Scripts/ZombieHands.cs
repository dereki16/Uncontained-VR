using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("XR/XR Socket Interactor")]

public class ZombieHands : MonoBehaviour
{
    public ZombieAction zombie;
    private GameObject plank;
    private List<GameObject> plankList = new List<GameObject>();
    private GameObject window;
    private bool zombieHandsHit;

    private float timer;
    private float timeAllotted;
    private bool dequeue;

    private int plankCount;
    private GameObject tailList;
    private Vector3 ogCenter;

    private float plankTimer;
    private float plankTimeAllotted;

    public PlayerHealth ph;

    void Start()
    {
        timeAllotted = 1.5f;
        timer = timeAllotted;

        plankTimeAllotted = 0.1f;
        plankTimer = plankTimeAllotted;
    }

    void Update()
    {
        if (plank != null && plank.GetComponent<BoxCollider>().center != ogCenter)
        {
            if (plankTimer > 0)
                plankTimer -= Time.deltaTime;
            else
            {
                Destroy(plank);
                plankTimer = plankTimeAllotted;
            }
        }

        if (zombie.attackBarricade)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (plank != null)
                {
                    plank.GetComponent<BoxCollider>().center = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
                }
                timer = timeAllotted;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plank"))
        {
            plank = other.gameObject;
            plankList.Add(other.gameObject);
            plankCount = plankList.Count;
            tailList = plankList[plankCount - 1];
            zombieHandsHit = true;
            dequeue = true;
            ogCenter = plank.GetComponent<BoxCollider>().center;
        }

        if (other.gameObject.CompareTag("Window"))
        {
            window = other.gameObject;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("PlayerHit");
            if (ph.health != null)
                ph.health -= 5;
        }
    }
}
