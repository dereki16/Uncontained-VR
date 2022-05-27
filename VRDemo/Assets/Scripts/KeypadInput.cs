using UnityEngine;
using TMPro;

public class KeypadInput : MonoBehaviour
{
    public string comboEntered;
    public TextMeshProUGUI comboDisplay;
    public bool check;
    public bool isColliding;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KeypadButton"))
        {
            comboEntered += other.gameObject.name;
            comboDisplay.text += comboEntered;
            comboEntered = "";
            FindObjectOfType<AudioManager>().Play("KeypadBeep");
        }

        if (other.gameObject.CompareTag("Enter"))
        {
            check = true;
        }

        if (other.gameObject.CompareTag("Back"))
        {
            comboEntered = "";
            comboDisplay.text = comboEntered;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("KeypadButton"))
        {
            isColliding = false;
        }
    }
}
