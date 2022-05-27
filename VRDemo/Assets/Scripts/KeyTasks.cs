using UnityEngine;

public class KeyTasks : TutorialController
{
    public SafeCombo combo;

    private void Awake()
    {
        combo = GameObject.FindGameObjectWithTag("SCS").GetComponent<SafeCombo>();
    }
    private void Update()
    {
        if (combo.keyEntered)
            TaskCompleted(img1, obj1, txt1);
    }
}
