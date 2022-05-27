using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

[AddComponentMenu("XR/XR Interaction Manager")]

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    public bool gripping;
    public bool handRotating;
    public bool rightHandRotating;
    public bool usingLeftController;
    public bool pullingTrigger;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    private Quaternion primaryRotation;
    private string controllerUsed;
    public string controllerTriggered;

    public GameObject[] plank;
    public GameObject hammer;
    public GameObject instHammer;

    public bool leftHandGripping;
    private GameObject instPlank;

    public int element;
    public PlankPooler pp;

    public int counter;

    public bool taskMove;
    public bool taskOrient;

    public MovementTasks mt;
    public LefthandTasks lt;
    public RighthandTasks rt;
    public PauseMenu pm;

    public bool lhRotating, lhGripping;
    public bool rhRotating, rhGripping;

    public string scene;

    public GameObject pauseCanvas;
    public DeathBoard db;

    public void Awake()
    {
        scene = SceneManager.GetActiveScene().name;
    }

    void Start()
    {
        TryInitialize();
        if (hammer == null)
        {
            hammer = GameObject.FindGameObjectWithTag("Hammer");
            if (hammer != null)
                hammer.SetActive(false);
        }

        pp = FindObjectOfType<PlankPooler>();
        mt = FindObjectOfType<MovementTasks>();
        pm = FindObjectOfType<PauseMenu>();
        if (scene == "Tutorial")
        {
            lt = GameObject.FindGameObjectWithTag("LT").GetComponent<LefthandTasks>();
            rt = GameObject.FindGameObjectWithTag("RT").GetComponent<RighthandTasks>();
        }
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
        controllerUsed = targetDevice.name;


        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.2f)
        {
            pullingTrigger = true;

            controllerTriggered = targetDevice.name;
        }
        else
            pullingTrigger = false;

        // and the grip
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.1f)
        {
            gripping = true;
        }
        else
            gripping = false;

        // as well as the controller being used
        if (targetDevice.name == "Oculus Touch Controller - Left controller")
            usingLeftController = true;
        else
            usingLeftController = false;

        // hand rotation
        if (targetDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion handRotation) && handRotation.x <= -0.8f)
        {
            handRotating = true;
        }
        else if (handRotation.x >= -0.5f)
        {
            handRotating = false;
            counter = 0;
        }

        if (controllerUsed == "Oculus Touch Controller - Left")
        {
            if (handRotating)
            {
                lhRotating = true;
                if (gripping && counter < 1)
                {
                    lhGripping = true;
                    instPlank = PlankPooler.sharedInstance.GetPooledObject();
                    element = pp.pooledObjects.IndexOf(instPlank);
                    pp.pooledObjects.Remove(pp.pooledObjects[element]);
                    if (instPlank != null)
                    {
                        instPlank.SetActive(true);
                        instPlank.transform.position = transform.position;
                        counter++;
                    }
                    if (lt != null)
                        lt.Task1();
                }
            }
            if (gripping)
            {
                if (instPlank != null)
                {
                    instPlank.transform.position = transform.position;
                    instPlank.transform.rotation = transform.rotation;
                }
            }
            if (scene == "Tutorial" && targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 moving) && moving != new Vector2(0f, 0f))
            {
                taskMove = true;
                mt.ChangedLocation();
            }
        }

        if (controllerUsed == "Oculus Touch Controller - Right")
        {
            if (handRotating)
            {
                rhRotating = true;
                if (gripping)
                    hammer.SetActive(true);
            }
            if (gripping)
            {
                rhGripping = true;
                hammer.transform.position = transform.position;
                if (rt != null)
                    rt.Task1();
            }

            if (scene == "Tutorial" && targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 moving) && moving != new Vector2(0f, 0f))
            {
                taskOrient = true;
                mt.ChangedOrientation();
            }
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool paused) && paused == true)
        {
            db = FindObjectOfType<DeathBoard>();
            if (db == null)
            {
                pm.pauseCanvas.SetActive(true);
                pm.Pause();
            }
            else if (db != null)
                return;
        }
    }
}
