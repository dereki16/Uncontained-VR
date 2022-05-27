using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public static int turning;
    public static float turningSpeed;
    public TextMeshProUGUI amount;

    private void Start()
    {
        turningSpeed = 5;
    }

    public void TurningContinuous()
    {
        // turn on continuous script, off snap turning
        turning = 0;
    }

    public void TurningSnap()
    {
        // turn on snap turning script, off continuous
        turning = 1;
    }

    public void LeftArrow()
    {
        if (turningSpeed >= 2)
            turningSpeed--;
        amount.text = turningSpeed.ToString();
    }

    public void RightArrow()
    {
        if (turningSpeed <= 10)
            turningSpeed++;
        amount.text = turningSpeed.ToString();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("turningStyle", turning);
        PlayerPrefs.SetFloat("turningSpeed", turningSpeed);
    }

    public void LoadSettings()
    {
        PlayerPrefs.GetInt("turningStyle");
        PlayerPrefs.GetInt("turningSpeed");
    }
}
