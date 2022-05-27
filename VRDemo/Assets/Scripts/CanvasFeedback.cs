using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.EventSystems;

public class CanvasFeedback : MonoBehaviour
{
    [SerializeField]
    XRBaseController controller;
    [SerializeField]
    XRBaseController controller2;

    public float strength;
    public float duration;

    public float defaultLength = 5f;
    public GameObject dot;
    public PointerInputModule inputModule;
    private LineRenderer lineRenderer = null;

    private bool enableHapticFeedback;
    public string deviceName;
    public bool trigger;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    protected void Start()
    {
        if (controller == null)
            Debug.LogWarning("Reference to the Controller is not set in the Inspector window, this behavior will not be able to send haptics. Drag a reference to the controller that you want to send haptics to.", this);

        strength = 0.3f;
        duration = 0.1f;
    }

    private void Update()
    {
        UpdateLine();
        deviceName = FindObjectOfType<HandPresence>().controllerTriggered;
        trigger = FindObjectOfType<HandPresence>().pullingTrigger;
    }

    private void UpdateLine()
    {
        float targetLength = defaultLength;
        RaycastHit hit = CreateRaycast(targetLength);
        Vector3 endPos = transform.position + (transform.forward * targetLength);
        dot.transform.position = endPos;

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Button") && enableHapticFeedback)
            {
                SendLowHaptics();
                enableHapticFeedback = false;
            }
            else if (!hit.collider.CompareTag("Button"))
                enableHapticFeedback = true;
        }
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);
        return hit;
    }

    public void SendHaptics()
    {
        if (controller != null)
            controller.SendHapticImpulse(strength, duration);
    }

    public void SendLowHaptics()
    {
        if (controller != null)
            controller.SendHapticImpulse(0.1f, 0.1f);
    }
}