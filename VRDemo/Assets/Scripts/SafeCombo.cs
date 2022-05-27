using UnityEngine;

public class SafeCombo : MonoBehaviour
{
    public CombinationRandomizer cr;
    public KeypadInput key;
    public string comboValueText;

    public Transform safeDoorOpen;

    public GameObject keycard;
    public Transform keycardPosition;

    public bool keyEntered;

    private void Start()
    {
        keycard.transform.position = keycardPosition.position;
        keycard.transform.rotation = keycardPosition.transform.localRotation;
    }

    private void EnterPin(int input)
    {
        if (input != cr.combo)
        {
            FindObjectOfType<AudioManager>().Play("KeypadWrongAnswer");
        }
        else if (input == cr.combo)
        {
            FindObjectOfType<AudioManager>().Play("SafeOpen");

            keyEntered = true;
            transform.position = safeDoorOpen.position;
            transform.rotation = safeDoorOpen.transform.localRotation;
            keycard.SetActive(true);
        }
    }

    void Update()
    {
        if (key.check)
        {
            EnterPin(int.Parse(key.comboDisplay.text));
            key.check = false;
        }
    }
}
