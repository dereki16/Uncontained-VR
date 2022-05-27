using UnityEngine;

public class OnTouchFloor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plank"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hammer"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
