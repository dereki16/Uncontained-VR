using UnityEngine;

public class DoorTasks : TutorialController
{
    public GameObject door;
    public GameObject canvas;

    private void Update()
    {
        if (door.activeInHierarchy == false)
        {
            canvas.SetActive(true);
            TaskCompleted(img1, obj1, txt1);
        }
    }
}
