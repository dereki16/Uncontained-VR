using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedController : MonoBehaviour
{
    [SerializeField]
    XRBaseController leftController;
    [SerializeField]
    XRBaseController rightController;

    public string deviceName;

    void Update()
    {
        deviceName = FindObjectOfType<HandPresence>().controllerTriggered;

        SendHaptics(deviceName);
        Debug.Log(deviceName);
    }

    public void SendHaptics(string device)
    {
        if (device == "Oculus Touch Controller - Left")
            FindObjectOfType<HapticFeedback>().SendHaptics();
        else if (device == "Oculus Touch Controller - Right")
            FindObjectOfType<HapticFeedback>().SendHaptics();
    }
}
