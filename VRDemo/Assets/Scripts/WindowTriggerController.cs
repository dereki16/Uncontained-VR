using UnityEngine;

public class WindowTriggerController : MonoBehaviour
{
    public GameObject trigger;
    public GameObject navMesh;
    private GameObject plank;
    private GameObject zombie;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WindowStopPoint"))
        {
            zombie = other.gameObject;
        }
        if (other.gameObject.CompareTag("Plank") && trigger != null)
        {
            plank = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie") && trigger != null)
        {
            zombie = other.gameObject;
        }
    }
}
