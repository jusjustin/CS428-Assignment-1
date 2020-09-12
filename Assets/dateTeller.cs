using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dateTeller : MonoBehaviour
{
    public GameObject dateTextObject;

    // Start is called before the first frame update
    void Start()
    {
    InvokeRepeating("UpdateTime", 0f, 10f);   
    }

    // Update is called once per frame
    void UpdateTime()
    {
    dateTextObject.GetComponent<TextMeshPro>().text = System.DateTime.Now.ToString("MM") + "/" + System.DateTime.Now.ToString("dd") + "/"  + System.DateTime.Now.ToString("yy");
    }
}