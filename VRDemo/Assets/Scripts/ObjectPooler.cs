using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler sharedInstance;
    public ZombieSpawner zs;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountInPool;

    [SerializeField]
    private GameObject zombieWoman;
    [SerializeField]
    private GameObject zombieMan;
    [SerializeField]
    private GameObject zombieCop;
    [SerializeField]
    private GameObject zombiePatient;

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
        ObjectPool(zombieWoman, amountInPool);
        ObjectPool(zombieMan, amountInPool);
        ObjectPool(zombieCop, amountInPool);
        ObjectPool(zombiePatient, amountInPool);
    }

    public GameObject GetPooledObject()
    {
        for (int ii = 0; ii < pooledObjects.Count; ii++)
        {
            if (pooledObjects[ii] != null)
            {
                if (!pooledObjects[ii].activeInHierarchy)
                {
                    randPooledObj = Random.Range(0, pooledObjects.Count);
                    return pooledObjects[randPooledObj];
                }
            }
        }
        return null;
    }
}
