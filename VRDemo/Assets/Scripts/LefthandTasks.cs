using UnityEngine;

public class LefthandTasks : TutorialController
{
    public CubeTutorial cube;
    public Canvas canvas;

    private void Update()
    {
        if (cube.plankPlaced)
            SetupRHT();
    }

    public void Task1()
    {
        TaskCompleted(img1, obj1, txt1);
    }

    public void SetupRHT()
    {
        TaskCompleted(img2, obj2, txt2);
        canvas.enabled = true;
    }
}
