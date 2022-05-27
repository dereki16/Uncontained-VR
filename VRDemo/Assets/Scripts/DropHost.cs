using UnityEngine;

public class DropHost : MonoBehaviour
{
    public GameObject[] objs;
    public GameObject[] objsParent;
    private float timer;
    private float timeAllotted;

    void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("Window");
        objsParent = GameObject.FindGameObjectsWithTag("WindowBox");
        timeAllotted = 2.5f;
        timer = timeAllotted;
    }

    void Update()
    {
        for (int ii = 0; ii < objs.Length; ii++)
        {
            if (!objs[ii].active)
            {
                if (timer > 0)
                    timer -= Time.deltaTime;
                else
                {
                    objs[ii].SetActive(true);
                    timer = timeAllotted;
                }
            }
        }

        for (int ii = 0; ii < objsParent.Length; ii++)
        {
            if (!objsParent[ii].active)
            {
                if (timer > 0)
                    timer -= Time.deltaTime;
                else
                {
                    objsParent[ii].SetActive(true);
                    timer = timeAllotted;
                }
            }
        }
    }
}
