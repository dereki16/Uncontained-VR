using UnityEngine;
public class DoorController : MonoBehaviour
{
    public GameObject door;
    public Light light;
    public bool canOpen;
    public bool touchingDoorknob;

    void Update()
    {
        // if player has 1000 pts && canOpen = true
        if (PointsFromBullets.points >= 1000 && touchingDoorknob)
        {
            door.SetActive(false);
            canOpen = true;
            if (canOpen)
                light.color = Color.green;

            PointsFromBullets.points -= 1000;
        }
        else if (PointsFromBullets.points <= 1000 && touchingDoorknob)
        {
            canOpen = false;
            if (!canOpen)
                light.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fingertip"))
            touchingDoorknob = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fingertip"))
            touchingDoorknob = false;
    }
}
