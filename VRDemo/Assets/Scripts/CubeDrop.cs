using System.Collections.Generic;
using UnityEngine;

public class CubeDrop : MonoBehaviour
{
    public List<GameObject> plankList = new List<GameObject>();
    public bool addPlank;

    private void Update()
    {
        if (plankList != null)
        {
            for (int ii = 0; ii < plankList.Count; ii++)
            {
                if (plankList[ii].active == false)
                {
                    plankList.Remove(plankList[ii]);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plank"))
        {
            FindObjectOfType<AudioManager>().Play("WoodenPlop");

            plankList.Add(other.gameObject);
            addPlank = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Plank"))
        {
            plankList.Remove(other.gameObject);
            addPlank = false;
        }
    }
}
