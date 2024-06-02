using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject damageMassage;

    List<TMPro.TextMeshPro> messagePool;

    int objectCount = 10;
    int count;

    private void Start()
    {
        messagePool = new List<TMPro.TextMeshPro>();

        for(int i = 0; i < objectCount; i++)
        {
            Populate();
        }
    }


    public void Populate()
    {
        GameObject go = Instantiate(damageMassage, transform);
        messagePool.Add(go.GetComponent<TMPro.TextMeshPro>());
        go.SetActive(false);
    }


    public void PostMassage(string text, Vector3 wordlPosition) 
    {
        messagePool[count].gameObject.SetActive(true);
        messagePool[count].transform.position = wordlPosition;
        messagePool[count].text = text;
        count += 1;

        if(count >= objectCount) 
        {
            count = 0;
        }
        //go.transform.position = wordlPosition;
        //go.GetComponent<TMPro.TextMeshPro>().text = text;
    }
}
