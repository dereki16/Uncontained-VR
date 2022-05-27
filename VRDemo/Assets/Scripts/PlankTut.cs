using UnityEngine;

public class PlankTut : MonoBehaviour
{
    public bool hammeredIn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hammer"))
        {
            hammeredIn = true;
        }
    }
}
