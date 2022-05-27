using UnityEngine;

public class PointsFromBullets : MonoBehaviour
{
    public static int points;
    public static int totalPoints;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Gunshot");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckForPlanks"))
        {
            points += 20;
            totalPoints += 20;
            this.gameObject.SetActive(false);
        }
    }
}
