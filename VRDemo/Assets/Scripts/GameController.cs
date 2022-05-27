using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject leftRay;
    public GameObject rightRay;
    void Start()
    {
        leftRay.SetActive(false);
        rightRay.SetActive(false);
        FindObjectOfType<AudioManager>().Play("RoundOver");
    }
}
