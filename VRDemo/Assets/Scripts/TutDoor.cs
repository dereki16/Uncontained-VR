using UnityEngine;

public class TutDoor : MonoBehaviour
{
    public GameObject door;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            if (PointsFromBullets.points >= 105)
            {
                PointsFromBullets.points -= 105;
                door.SetActive(false);
            }
        }
    }
}
