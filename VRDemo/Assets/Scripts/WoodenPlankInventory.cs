using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WoodenPlankInventory : MonoBehaviour
{
    public PlankPooler pp;
    public int element;
    public GameObject tmpPlank;
    public GameObject plank;
    public GameObject plankIn;
    public Transform attachTransform;

    public XRSocketInteractor socket;
    public XRBaseInteractable interactable;
    public XRInteractionManager interactionManager;

    public bool plankInHand;
    public bool plankInHand2;

    public bool addPlank;

    public float timer;
    public float timeAllotted;

    private void Start()
    {
        if (plank != null)
            plankInHand = plank.GetComponent<WoodenPlank>().grabbingOntoPlank;

        timeAllotted = 1f;
        timer = timeAllotted;
    }
    protected virtual void OnSelectExiting(XRBaseInteractable interactable)
    {
        plank = PlankPooler.sharedInstance.GetPooledObject();
        pp.pooledObjects.Remove(pp.pooledObjects[element]);

        if (plank != null)
        {
            plankInHand2 = plank.GetComponent<WoodenPlank>().grabbingOntoPlank;
            interactable = plank.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRBaseInteractable>();
            interactionManager.ForceSelect(socket, interactable);
   
            plank.SetActive(true);
        }
    }

    void Update()
    {
        if (addPlank)
        {
            plank = PlankPooler.sharedInstance.GetPooledObject();

            if (plank != null)
            {
                plank.transform.position = attachTransform.transform.position;
                plank.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Plank"))
        {
            addPlank = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Plank"))
        {
            addPlank = true;
        }
    }
}
