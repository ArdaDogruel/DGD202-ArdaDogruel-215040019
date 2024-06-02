using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    float timeToDisable = 0.1f;
    float timer;


    private void OnEnable()
    {
        timer = timeToDisable;
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if(timer < 0f) 
        {
            gameObject.SetActive(false);
        }
    }

}
