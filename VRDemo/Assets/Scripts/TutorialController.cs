using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Image img1;
    public Image img2;
    public GameObject obj1;
    public GameObject obj2;
    public TextMeshProUGUI txt1;
    public TextMeshProUGUI txt2;
    public void TaskCompleted(Image img, GameObject obj, TextMeshProUGUI txt)
    {
        img.color = Color.red;
        obj.SetActive(true);
        txt.fontStyle = FontStyles.Strikethrough;
    }
}
