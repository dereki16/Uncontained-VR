using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeCodeLocation : MonoBehaviour
{
    public SafeLocation sl;
    public Transform pos0;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;

    public GameObject note;

    public string scene;
    public int num;

    public Vector3 NoteLoc()
    {
        if (scene == "Tutorial")
            num = 3;
        else
            num = sl.randNum;

        switch (num)
        {
            case 0:
                // kitchen
                note.transform.position = pos0.position;
                break;
            case 1:
                // bathroom
                note.transform.position = pos1.position;

                break;
            case 2:
                // boss' office
                note.transform.position = pos2.position;
                break;
            case 3:
                // tutorial
                note.transform.position = pos3.position;
                break;
            default:
                break;
        }
        return note.transform.position;
    }
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        NoteLoc();
    }
}
