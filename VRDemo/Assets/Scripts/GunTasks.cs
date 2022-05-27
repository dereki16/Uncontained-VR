using UnityEngine;

public class GunTasks : TutorialController
{
    public GameObject canvas;
    public void GunGripped()
    {
        TaskCompleted(img1, obj1, txt1);
    }

    public void GunTriggered()
    {
        TaskCompleted(img2, obj2, txt2);
        canvas.SetActive(true);
    }
}
