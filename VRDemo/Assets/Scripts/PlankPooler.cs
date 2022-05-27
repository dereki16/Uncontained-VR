using System.Collections.Generic;
using UnityEngine;

public class PlankPooler : MonoBehaviour
{
    public static PlankPooler sharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountInPool;

    [SerializeField]
    private GameObject plank;

    GameObject obj;
    public int randPooledObj;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void ObjectPool(GameObject objectPool, int amountInPool)
    {
        for (int ii = 0; ii < amountInPool; ii++)
        {
            obj = (GameObject)Instantiate(objectPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    private void Start()
    {
        ObjectPool(plank, amountInPool);
    }

    public GameObject GetPooledObject()
    {
        for (int ii = 0; ii < pooledObjects.Count; ii++)
        {
            if (!pooledObjects[ii].activeInHierarchy)
            {
                return pooledObjects[ii];
            }
        }
        return null;
    }
}
