using UnityEngine;

public class PlayAmbience : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Ambience");
    }
}