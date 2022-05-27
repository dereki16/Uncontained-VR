using System.Collections.Generic;
using UnityEngine;

public class RoomRandomizer : MonoBehaviour
{
    public GameObject mainRoom;
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public GameObject room4;
    public GameObject room5;

    private GameObject spawnedMainRoom;
    private GameObject spawnedRoom1;
    private GameObject spawnedRoom2;
    private GameObject spawnedRoom3;
    private GameObject spawnedRoom4;
    private GameObject spawnedRoom5;
    private GameObject[] rooms;


    public Transform mainRoomT1;
    public Transform mainRoomT2;
    public Transform room1T1;
    public Transform room1T2;
    public Transform room2T1;
    public Transform room2T2;

    public Transform room3T1;
    public Transform room3T2;
    public Transform room4T1;
    public Transform room4T2;
    public Transform room5T1;
    public Transform room5T2;

    private int[] list;
    private List<int> intList = new List<int>();
    private List<int> randList = new List<int>(5);
    private int randInt;

    private int randomizer;
    void Start()
    {
        spawnedMainRoom = (GameObject)Instantiate(mainRoom);
        spawnedRoom1 = (GameObject)Instantiate(room1);
        spawnedRoom2 = (GameObject)Instantiate(room2);
        spawnedRoom3 = (GameObject)Instantiate(room3);
        spawnedRoom4 = (GameObject)Instantiate(room4);
        spawnedRoom5 = (GameObject)Instantiate(room5);

        spawnedMainRoom.SetActive(false);
        spawnedRoom1.SetActive(false);
        spawnedRoom2.SetActive(false);
        spawnedRoom3.SetActive(false);
        spawnedRoom4.SetActive(false);
        spawnedRoom5.SetActive(false);

        OrganizeRooms();
    }

    private void RandomizeRooms()
    {
        for (int ii = 0; ii < 5; ii++)
        {
            randInt = Random.Range(0, 5);
            if (randList == null || randList[ii] != randInt)
            {
                randList.Add(randInt);
            }
            Debug.Log(randList[ii]);
        }
    }

    private void SpawnedRooms(GameObject spawnedRoom, Transform transform, float sign)
    {
        spawnedRoom.transform.position = new Vector3(transform.position.x + 0.5f * sign, 0f, 0f);
        spawnedRoom.SetActive(true);
    }

    private void OrganizeRooms()
    {
        int roomLoadout = 0;
        switch (roomLoadout)
        {
            case 0:
                {
                    spawnedMainRoom.SetActive(true);
                    SpawnedRooms(spawnedRoom1, mainRoomT1, -1f);
                    SpawnedRooms(spawnedRoom2, mainRoomT2, 1f);
                    SpawnedRooms(spawnedRoom3, room1T1, -1f);
                    break;
                }
            default:
                break;
        }
        
    }
}
