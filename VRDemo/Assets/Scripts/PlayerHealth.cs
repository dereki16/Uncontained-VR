using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;

    public float hitTimer;
    public float hitTimeAllotted;

    public bool canHit = true;
    public bool hasBeenHit;

    private float regenerateTimer;
    private float regenerateTimeAllotted;

    public int counter;

    public GameObject deathBoard;
    public GameObject leftRay;
    public GameObject rightRay;

    public static int finalPoints;

    public GameObject blood1;
    public GameObject blood2;
    public GameObject blood3;
    public GameObject blood4;
    public GameObject bloodInst;
    public GameObject player;
    public Transform playersFace;

    public int bloodCounter;
    public Vector3 offset;
    public Vector3 offset1;
    public Vector3 offset2;
    public Vector3 offset3;
    public Vector3 offset4;

    void Start()
    {
        hitTimeAllotted = 0.5f;
        hitTimer = hitTimeAllotted;

        regenerateTimeAllotted = 3.5f;
        regenerateTimer = regenerateTimeAllotted;
    }

    public void ActivateBlood(GameObject obj, Vector3 offset)
    {
        obj.SetActive(true);
        obj.transform.position = playersFace.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        /*        bloodInst = GameObject.Instantiate(blood);
                bloodInst.transform.position = playersFace.transform.position;
                bloodInst.transform.rotation = player.transform.rotation;*/



        switch (bloodCounter)
        {
            case 1:
                ActivateBlood(blood1, offset1);
                break;
            case 2:
                ActivateBlood(blood2, offset2);
                break;
            case 3:
                ActivateBlood(blood3, offset3);
                break;
            case 4:
                ActivateBlood(blood4, offset4);
                break;
            case 5:
                blood1.SetActive(false);
                blood2.SetActive(false);
                blood3.SetActive(false);
                blood4.SetActive(false);
                bloodCounter = 0;
                break;
            default:
                break;
        }
        if (bloodInst != null)
        {
            
        }

        if (hasBeenHit)
        {
            // allows zombies to hit player and deal damage
            if (hitTimer > 0f)
            {
                hitTimer -= Time.deltaTime;
            }
            else
            {
                canHit = true;
                hitTimer = hitTimeAllotted;
            }

            // begin health regen after 3.5 seconds of not being hit
            if (counter < 2)
            {
                if (regenerateTimer > 0f)
                {
                    regenerateTimer -= Time.deltaTime;
                }
                else
                {
                    // add health back up to 100
                    health += 0.1f;
                    if (health >= 100)
                    {
                        regenerateTimer = regenerateTimeAllotted;
                        hasBeenHit = false;
                        Destroy(bloodInst);
                    }
                }
            }
            else
            {
                counter = 0;
                regenerateTimer = regenerateTimeAllotted;
            }
        }

        if (health <= 0) 
        {
            deathBoard.SetActive(true);
            leftRay.SetActive(true);
            rightRay.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            hasBeenHit = true;
            counter++;
            bloodCounter++;
            if (canHit)
            {
                health -= 5;
                FindObjectOfType<AudioManager>().Play("PlayerHit");
            }
        }
    }
}
