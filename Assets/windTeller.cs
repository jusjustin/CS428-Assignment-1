using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System.Diagnostics;
using System.Globalization;

public class windTeller : MonoBehaviour
{
    public GameObject windTextObject;
    public GameObject flag;
    public GameObject flagGameObject;
    private string speed = "00";
    private string degree = "00";
    private string degreeToDirection = "X";
    private int indexS;
    private int indexD;
       string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=aaab1c2305b0c882a7b68e379529aa91&units=imperial";

   
    void Start()
    {
       // wait a couple seconds to start and then refresh every 900 seconds

       InvokeRepeating("GetDataFromWeb", 2f, 900f);
       InvokeRepeating("UpdateTime", 0f, 10f);   
   }

   void GetDataFromWeb()
   {

       StartCoroutine(GetRequest(url));
   }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                //Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                indexS = webRequest.downloadHandler.text.IndexOf("\"speed\":") + 8;
                indexD = webRequest.downloadHandler.text.IndexOf("\"deg\":") + 6;
                speed = webRequest.downloadHandler.text.Substring(indexS, 2);
                degree  = webRequest.downloadHandler.text.Substring(indexD, 2);
            }
        }
    }
  
    // Update is called once per frame
    void UpdateTime()
    {
    int temp_Degree = System.Convert.ToInt32(degree);

    if(temp_Degree >= 0 && temp_Degree <= 45)
    {degree = "N";}
    else if(temp_Degree > 45 && temp_Degree <= 90)
    {degree = "E";}
    else if(temp_Degree > 90 && temp_Degree <= 135)
    {degree = "E";}
    else if(temp_Degree > 135 && temp_Degree <= 180)
    {degree = "S";}
    else if(temp_Degree > 180 && temp_Degree <= 225)
    {degree = "S";}
    else if(temp_Degree > 225 && temp_Degree <= 270)
    {degree = "W";}
    else if(temp_Degree > 270 && temp_Degree <= 315)
    {degree = "W";}
    else
    {degree = "N";}

    flagGameObject.GetComponent<Transform>().transform.Rotate(0, temp_Degree, 0);
    flag.GetComponent<Transform>().transform.Rotate(0, 0, float.Parse(speed));
    windTextObject.GetComponent<TextMeshPro>().text = speed + " mph " + degree;
    
    }
}