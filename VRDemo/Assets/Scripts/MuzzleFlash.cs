using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    private float timer;
    void Start()
    {
        timer = 1.2f;
    }

    void Update()
    {
        if (timer > Time.deltaTime)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
