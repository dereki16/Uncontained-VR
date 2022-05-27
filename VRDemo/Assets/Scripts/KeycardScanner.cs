using UnityEngine;

public class KeycardScanner : MonoBehaviour
{
    public Light feedbackLight;
    public bool openElevators;
    public bool close;
    public bool onMusic;
    public bool onDing;

    public GameObject door1;
    public GameObject door2;

    public Transform pos1;
    public Transform pos2;
    public Transform ogPos1;
    public Transform ogPos2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Keycard"))
        {
            feedbackLight.color = Color.green;
            openElevators = true;
            onMusic = true;
            onDing = true;
        }
        else if (!other.gameObject.CompareTag("Keycard"))
        {
            feedbackLight.color = Color.red;
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            close = true;
            openElevators = false;
        }
    }

    private void Update()
    {
        if (openElevators)
        {
            door1.transform.position = pos1.position;
            door2.transform.position = pos2.position;
        }
        else if (close)
        {
            door1.transform.position = ogPos1.position;
            door2.transform.position = ogPos2.position;
            close = false;
        }
        if (onMusic)
        {
            FindObjectOfType<AudioManager>().Play("ElevatorMusic");
            onMusic = false;
        }
        if (onDing)
        {
            FindObjectOfType<AudioManager>().Play("ElevatorDing");
            onDing = false;
        }
    }
}
