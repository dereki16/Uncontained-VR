using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class GameManager : OptionsMenu
{
    public GameObject xrrig;

    private void Awake()
    {
        if (turning == 0)
        {
            xrrig.GetComponent<ContinuousTurnProviderBase>().enabled = true;
            xrrig.GetComponent<SnapTurnProviderBase>().enabled = false;
            xrrig.GetComponent<ContinuousTurnProviderBase>().turnSpeed = turningSpeed * 8;
        }
        else if (turning == 1)
        {
            xrrig.GetComponent<ContinuousTurnProviderBase>().enabled = false;
            xrrig.GetComponent<SnapTurnProviderBase>().enabled = true;
            xrrig.GetComponent<SnapTurnProviderBase>().turnAmount = turningSpeed * 5;
        }
    }
}
