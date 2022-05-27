using UnityEngine;

public class ScanTasks : TutorialController
{
    public KeycardScanner scan;

    private void Awake()
    {
        scan = GameObject.FindGameObjectWithTag("Scanner").GetComponent<KeycardScanner>();
    }

    private void Update()
    {
        if (scan.openElevators)
            TaskCompleted(img1, obj1, txt1);
    }
}
