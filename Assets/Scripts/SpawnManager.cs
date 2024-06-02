using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnObject(Vector3 worldPosition, GameObject toSpawn)
    {
        Transform t = Instantiate(toSpawn, transform).transform;
        t.position = worldPosition;
    }
}
