using UnityEngine;

public class PointsTask : TutorialController
{
    public GameObject canvas;
    public Canvas canvas2;

    public void NoteSelected()
    {
        TaskCompleted(img1, obj1, txt1);
        canvas.SetActive(true);
        canvas2.enabled = true;
    }
}
