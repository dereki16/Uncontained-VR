using UnityEngine;

public class NavMeshCollider : MonoBehaviour
{
    public bool plankAttached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plank"))
        {
            plankAttached = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Plank"))
        {
            plankAttached = false;
        }
    }
}
