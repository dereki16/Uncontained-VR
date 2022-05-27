using UnityEngine;

public class RighthandTasks : TutorialController
{
    public GameObject canvas;
    public void Task1()
    {
        TaskCompleted(img1, obj1, txt1);
    }

    public void SetupDoor()
    {
        TaskCompleted(img2, obj2, txt2);
        canvas.SetActive(true);
    }
}
