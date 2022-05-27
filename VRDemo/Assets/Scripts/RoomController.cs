using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject doorToCafe;
    public GameObject doorToMainOffice;
    public GameObject doorToBossOffice;
    public GameObject doorToBathroom;

    public GameObject Cafe;
    public GameObject MainOffice;
    public GameObject BossOffice;
    public GameObject Bathroom;

    private void Start()
    {
        Cafe.SetActive(false);
        MainOffice.SetActive(false);
        BossOffice.SetActive(false);
        Bathroom.SetActive(false);
    }

    void Update()
    {
        if (doorToCafe.active == false)
            Cafe.SetActive(true);
        if (doorToMainOffice.active == false)
            MainOffice.SetActive(true);
        if (doorToBossOffice.active == false)
            BossOffice.SetActive(true);
        if (doorToBathroom.active == false)
            Bathroom.SetActive(true);
    }
}
