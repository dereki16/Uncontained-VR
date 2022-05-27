using UnityEngine;

public class CubeTutorial : MonoBehaviour
{
    public bool plankPlaced;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plank"))
        {
            plankPlaced = true;
        }
    }
}
