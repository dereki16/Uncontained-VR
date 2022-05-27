using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static int wave = 1;
    public int zombiesToSpawn;
    public Transform[] spawnPoint;
    public GameObject zombie;
    public ObjectPooler op;

    public float timer;
    private float slowTimer = 10f;
    private float timeBetweenSpawns;
    private bool timeToSpawn;
    public bool newRound;
    public bool slowSpawn;

    private int randSpawnPoint;
    private int zombiesInScene;
    private int healthInc;
    public float speedInc;
    private int zombieInc;

    private float zombieSpeed;
    private float randSpeed;
    public int health;
    public int element;
    public float newRoundTimer;

    private List<GameObject> zombieList = new List<GameObject>();
    public RoomController rc;

    void Start()
    {
        timeBetweenSpawns = Random.Range(1f, 10f);
        timer = timeBetweenSpawns;
    }

    void Update()
    {
        randSpeed = Random.Range(1.5f, 2f);
        zombieSpeed = randSpeed;

        // Spawn x number of zombies every few seconds at rand spawnPoints -> increase x every wave
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timeToSpawn = false;
        }
        else
        {
            // Spawn slow or fast depending on whether a new round has begun
            if (slowSpawn)
            {
                timeBetweenSpawns = slowTimer;
                timeToSpawn = false;
            }
            else
            {
                timeBetweenSpawns = Random.Range(1f, 10f);
                timeToSpawn = true;
            }
            slowSpawn = false;
            timer = timeBetweenSpawns;
        }

        if (timeToSpawn && zombiesToSpawn > 0)
        {
            slowSpawn = false;

            // spawn
            zombiesToSpawn--;

            zombie = ObjectPooler.sharedInstance.GetPooledObject();
            element = op.pooledObjects.IndexOf(zombie);
            op.pooledObjects.Remove(op.pooledObjects[element]);
            if (zombie != null)
            {
                TransformsAvailable();
                zombie.transform.position = spawnPoint[randSpawnPoint].position;
                zombie.GetComponent<ZombieAction>().health = 10 + health;
                //zombie.GetComponent<ZombieAction>().agent.speed = 0.6f + speedInc;
                zombie.GetComponent<ZombieAction>().anim.speed += zombieSpeed + speedInc;
                //anim.speed = 2f + speedInc;
                zombie.SetActive(true);
                zombie.GetComponent<ZombieAction>().health = 10 + health;
            }
        }
        zombiesInScene = GameObject.FindGameObjectsWithTag("Zombie").Length;
        if (zombiesInScene == 0 && zombiesToSpawn == 0)
        {
            newRound = true;
            slowSpawn = true;
            newRoundTimer += Time.deltaTime;
            FindObjectOfType<AudioManager>().Play("RoundOver");
        }

        if (newRound)
        {
            wave++;
            health += 5;
            speedInc += 0.2f;

            if (healthInc <= 100)
                HealthIncrease();
            if (speedInc <= 2)
                SpeedIncrease();
            if (zombiesToSpawn <= 200)
            {
                zombiesToSpawn = ZombieIncrease();
            }
            else
                zombiesToSpawn = 200;
            newRound = false;
        }
    }

    public void TransformsAvailable()
    {
        randSpawnPoint = Random.Range(0, 3);

        if (rc.doorToCafe.active == false)
        {
            randSpawnPoint = Random.Range(0, 7);
        }
        else if (rc.doorToMainOffice.active == false)
        {
            randSpawnPoint = Random.Range(0, 11);
        }
        else if (rc.doorToBossOffice.active == false)
        {
            randSpawnPoint = Random.Range(0, 13);
        }
        else if (rc.doorToBathroom.active == false)
        {
            randSpawnPoint = Random.Range(0, spawnPoint.Length);
        }
    }

    public void HealthIncrease()
    {
        healthInc += 1;
    }

    public void SpeedIncrease()
    {
        speedInc += 0.02f;
    }

    public int ZombieIncrease()
    {
        zombieInc += Random.Range(4, 8);
        return zombieInc;
    }
}
