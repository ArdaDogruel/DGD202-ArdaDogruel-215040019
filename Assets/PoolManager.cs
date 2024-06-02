using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] GameObject poolPrefab;
    Dictionary<int, ObjectPool> poolList;

    private void Awake()
    {
        poolList = new Dictionary<int, ObjectPool>();
    }


    public void CreatePool(int newObjectPoolId)
    {
        GameObject newObjectPoolGO = Instantiate(poolPrefab, transform).gameObject;
        ObjectPool newObjectPool = newObjectPoolGO.GetComponent<ObjectPool>();
        poolList.Add(newObjectPoolId, newObjectPool);

    }

}
