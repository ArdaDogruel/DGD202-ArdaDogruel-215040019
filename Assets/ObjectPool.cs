using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    GameObject originalPrefab;

    List<GameObject> pool;

    public void Set(GameObject originalPrefab)
    {
        pool = new List<GameObject>();
        this.originalPrefab = originalPrefab;
    }

    public void InstantiateObject()
    {
        GameObject newObject = Instantiate(originalPrefab, transform);
        pool.Add( newObject );
    }
}
