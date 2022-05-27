using UnityEngine;
using TMPro;

public class PointsController : MonoBehaviour
{
    public TextMeshProUGUI pointsDisplay;

    void Update()
    {
        pointsDisplay.text = PointsFromBullets.points.ToString();
    }
}
