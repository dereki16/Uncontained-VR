using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeLocation : MonoBehaviour
{
    public Transform pos0;
    public Transform pos1;
    public Transform pos2;

    public GameObject safe;
    public GameObject spawnedSafe;
    public int randNum;

    public string scene;
    public int num;

    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        SafeLoc();
    }

    public Vector3 SafeLoc()
    {
        randNum = Random.Range(0, 3);
        if (scene == "Tutorial")
            num = 2;
        else
            num = randNum;
        switch (num)
        {
            case 0:
                safe.transform.position = pos0.position;
                safe.transform.localRotation = pos0.localRotation;
                break;
            case 1:
                safe.transform.position = pos1.position;
                safe.transform.localRotation = pos1.localRotation;

                break;
            case 2:
                safe.transform.position = pos2.position;
                safe.transform.localRotation = pos2.localRotation;
                break;
            default:
                break;
        }
        return safe.transform.position;
    }
}
