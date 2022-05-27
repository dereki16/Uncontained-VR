using UnityEngine;
using UnityEngine.UI;

public class Targets : MonoBehaviour
{
    public GameObject canvas;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (this.gameObject.GetComponent<Image>().color != Color.red)
            {
                PointsFromBullets.points += 20;
                PointsFromBullets.totalPoints += 20;
            }
            this.gameObject.GetComponent<Image>().color = Color.red;

        }
    }

    private void Update()
    {
        if (PointsFromBullets.points >= 100)
            canvas.SetActive(true);
    }
}
