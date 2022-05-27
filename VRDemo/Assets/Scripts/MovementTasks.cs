using UnityEngine;

public class MovementTasks : TutorialController
{
    public HandPresence hp;

    public void Start()
    {
        hp = FindObjectOfType<HandPresence>();
    }
    public void Update()
    {
        if (hp != null)
        {
            if (hp.taskMove)
                ChangedLocation();
            if (hp.taskOrient)
                ChangedLocation();
        }
    }
    public void ChangedLocation()
    {
        TaskCompleted(img1, obj1, txt1);
    }

    public void ChangedOrientation()
    {
        TaskCompleted(img2, obj2, txt2);
   }
}
