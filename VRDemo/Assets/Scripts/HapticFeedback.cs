using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedback : MonoBehaviour
{
    [SerializeField]
    XRBaseController controller;
    [SerializeField]
    XRBaseController controller2;

    public float strength;
    public float duration;

    public string deviceName;
    public bool trigger;

    public void Start()
    {
        if (controller == null)
            Debug.LogWarning("Reference to the Controller is not set in the Inspector window, this behavior will not be able to send haptics. Drag a reference to the controller that you want to send haptics to.", this);

        strength = 0.3f;
        duration = 0.1f;
    }

    public void Update()
    {
        deviceName = FindObjectOfType<HandPresence>().controllerTriggered;
        trigger = FindObjectOfType<HandPresence>().pullingTrigger;
    }

    public void SendHaptics()
    {
        if (trigger)
        {
            if (controller != null && deviceName == "Oculus Touch Controller - Left")
            {
                controller.SendHapticImpulse(strength, duration);
            }
            else if (controller != null && deviceName == "Oculus Touch Controller - Right")
            {
                controller2.SendHapticImpulse(strength, duration);
            }
            Debug.Log("Haptic wnet throgu");
        }
        DontDestroyOnLoad(this);
    }
}