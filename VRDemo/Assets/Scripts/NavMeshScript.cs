using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NavMeshScript : MonoBehaviour
{
    public CubeDrop cd;
    public ZombieSpawner zs;
    public NavMeshCollider col;

    private float timer;
    private float timeAllotted;
    private float meshTimer;
    private float meshTimeAllotted;

    private bool startTimer;
    private float navRefreshTimer;
    private float navRefreshTimeAllotted;
    private bool setMeshActive;
    public bool plankAttached;

    public GameObject navMesh;
    public GameObject cube;
    public GameObject[] planks;
    public List<GameObject> plankList = new List<GameObject>();

    public int counter;
    public int plankCount;

    public List<GameObject> cubeList = new List<GameObject>();

    public int tmp;

    private float plankTimeAllotted;
    private float plankTimer;

    void Start()
    {
        timeAllotted = 10f;
        timer = timeAllotted;

        meshTimeAllotted = 0.5f;
        meshTimer = meshTimeAllotted;

        plankTimeAllotted = 1f;
        plankTimer = plankTimeAllotted;

        navRefreshTimeAllotted = 10f;
        navRefreshTimer = navRefreshTimeAllotted;

        plankCount = plankList.Count;
        StartCoroutine(StartCountingPlanks());
    }

    IEnumerator StartCountingPlanks()
    {
        yield return new WaitForSeconds(1f);
        if (cubeList != null && plankList.Count <= 3)
        {
            for (int ii = 0; ii < cubeList.Count; ii++)
            {
                if (cubeList != null)
                    plankList.AddRange(cubeList[ii].GetComponent<CubeDrop>().plankList);
            }
        }
    }

    private void AmountOfTimeAttackingBarricade(int counter)
    {
        switch (counter)
        {
            case 0:
                timeAllotted = 0f;
                break;
            case 1:
                timeAllotted = 4f;
                break;
            case 2:
                timeAllotted = 6f;
                break;
            case 3:
                timeAllotted = 8f;
                break;
            case 4:
                timeAllotted = 10f;
                break;
            default:
                break;
        }
        timer = timeAllotted;
    }

    void Update()
    {
        if (navRefreshTimer > 0f)
        {
            navRefreshTimer -= Time.deltaTime;
        }
        else
        {
            navMesh.SetActive(false);
            navRefreshTimer = navRefreshTimeAllotted;
        }
        
        plankCount = plankList.Count;

        if (plankTimer > 0f)
        {
            plankTimer -= Time.deltaTime;
        }

        if (plankList.Count == 0)
        {
            plankAttached = false;
        }
        else if (plankList.Count >= 1)
        {
            plankAttached = true;
        }

        if (plankAttached)
        {
            navMesh.SetActive(false);
        }
        else if (!plankAttached)
        {
            navMesh.SetActive(true);
        }

        if (startTimer)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                cube.SetActive(true);
                setMeshActive = true;

                timer = timeAllotted;
                startTimer = false;
            }
        }

        if (setMeshActive)
        {
            if (meshTimer > 0)
            {
                meshTimer -= Time.deltaTime;
            }
            else
            {
                navMesh.SetActive(true);
                meshTimer = meshTimeAllotted;
                setMeshActive = false;
            }
        }

        for (int ii = 0; ii < plankList.Count; ii++)
        {
            if (plankList != null && plankList[ii].active == false)
            {
                plankList.Remove(plankList[ii]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckForPlanks"))
        {
            startTimer = true;
        }

        if (other.gameObject.CompareTag("Plank"))
        {
            plankList.Add(other.gameObject);
            plankAttached = true;
        }

        if (other.gameObject.CompareTag("WindowStopPoint"))
        {
            navMesh.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plank"))
        {
            //amountNotTriggering++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Plank"))
        {
            plankList.Remove(other.gameObject);
        }

        if (other.gameObject.CompareTag("WindowStopPoint"))
        {
            navMesh.SetActive(false);
        }
    }
}
