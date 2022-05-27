using UnityEngine;

public class PlayerBlood : MonoBehaviour
{
    public GameObject player;
    private void Update()
    {
        this.transform.rotation = player.transform.rotation;
    }
}
