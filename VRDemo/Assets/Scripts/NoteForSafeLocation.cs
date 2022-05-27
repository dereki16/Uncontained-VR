using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NoteForSafeLocation : MonoBehaviour
{
    public SafeLocation sl;
    public TextMeshProUGUI note;
    public string scene;
    public int num;

    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        NoteForSafe();
    }

    private void NoteForSafe()
    {
        if (scene == "Tutorial")
            num = 2;
        else
            num = sl.randNum;
        switch (num)
        {
            case 0:            
                note.text = "\n   Have the safe \n   moved by the \n   books in my \n   office ASAP! \n\t   - Boss";
                break;
            case 1:
                note.text = "\n   Have Jim in \n   the corner of \n   the main office \n   take care of \n   the safe.   \n\t   - Boss";
                break;
            case 2:
                note.text = "\n   Why is the \n   safe in the \n   kitchen? That \n   makes no \n   sense. Move it \n   or you're fired! \n\t   - Boss";
                break;
            default:
                break;
        }
    }
}
