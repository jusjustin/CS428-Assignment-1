using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System.Globalization;

public class tempatureTeller : MonoBehaviour
{
    public GameObject tempatureTextObject;
    public GameObject Thermometer_Outer_Cylinder;
    public GameObject Beaker_Outer_Cylinder;
    private Vector3 V3Thermometer_Outer_Cylinder;
    private Vector3 V3Beaker_Outer_Cylinder;
    
    private string tempature = "00";
    private string humidity = "00";
    private int indexT;
    private int indexH;
       string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=aaab1c2305b0c882a7b68e379529aa91&units=imperial";

   
    void Start()
    {
       V3Thermometer_Outer_Cylinder = Thermometer_Outer_Cylinder.GetComponent<Transform>().transform.localScale;
       V3Beaker_Outer_Cylinder = Beaker_Outer_Cylinder.GetComponent<Transform>().transform.localScale;
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
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                indexT = webRequest.downloadHandler.text.IndexOf("\"temp\":") + 7;
                indexH = webRequest.downloadHandler.text.IndexOf("\"humidity\":") + 11;
                tempature = webRequest.downloadHandler.text.Substring(indexT, 2);
                humidity  = webRequest.downloadHandler.text.Substring(indexH, 2);
            }
        }
    }
  
    // Update is called once per frame
    void UpdateTime()
    {
    tempatureTextObject.GetComponent<TextMeshPro>().text = tempature + "F\n" + humidity + "%";
    Thermometer_Outer_Cylinder.GetComponent<Transform>().transform.localScale = new Vector3(V3Thermometer_Outer_Cylinder.x, (V3Thermometer_Outer_Cylinder.y * float.Parse(("0." + tempature), CultureInfo.InvariantCulture)), V3Thermometer_Outer_Cylinder.z);
    Beaker_Outer_Cylinder.GetComponent<Transform>().transform.localScale = new Vector3(V3Beaker_Outer_Cylinder.x, (V3Beaker_Outer_Cylinder.y * float.Parse(("0." + humidity), CultureInfo.InvariantCulture)), V3Beaker_Outer_Cylinder.z);
    }
}