using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class iconTeller : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClipArray;
    public GameObject Sun;
    public GameObject Cloud;
    public GameObject Snow;
    public GameObject iconTextObject;
    private string iconID = "00";
    private int iconArrayIndex = 0;
    private string[] iconStringArray = {"01", "02", "03", "04", "09", "10", "11", "13", "50"};
    private int indexI;
    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=aaab1c2305b0c882a7b68e379529aa91&units=imperial";

   
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
       // wait a couple seconds to start and then refresh every 900 seconds
       InvokeRepeating("GetDataFromWeb", 2f, 900f);
       InvokeRepeating("UpdateTime", 0f, 3f);
   }

    void Update()
    {
        if (Input.GetKeyDown("left"))
        {
            Debug.Log("left arrow key is pressed");
            if(iconArrayIndex <= 0)
            {iconArrayIndex = 8;}
            else
            {iconArrayIndex -= 1;}
            audioSource.Stop();
            audioSource.clip = audioClipArray[iconArrayIndex];
            audioSource.Play();
        }

        if (Input.GetKeyDown("right"))
        {
            Debug.Log("right arrow key is pressed");
            if(iconArrayIndex >= 8)
            {iconArrayIndex = 0;}
            else
            {iconArrayIndex += 1;}
            audioSource.Stop();
            audioSource.clip = audioClipArray[iconArrayIndex];
            audioSource.Play();
        }
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
                //print out the weather data to make sure it makes sense
                //Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                indexI = webRequest.downloadHandler.text.IndexOf("\"icon\":") + 8;
                iconID = webRequest.downloadHandler.text.Substring(indexI, 2);
                //Animation
                Sun.gameObject.SetActive(false);
                Cloud.gameObject.SetActive(false);
                Snow.gameObject.SetActive(false);
                //Sound
                audioSource.Stop();
                audioSource.clip = audioClipArray[iconArrayIndex];
                audioSource.Play();

                if(string.Equals(iconID, "01"))
                    {iconArrayIndex = 0;}
                else if(string.Equals(iconID, "02"))
                    {iconArrayIndex = 1;}
                else if(string.Equals(iconID, "03"))
                    {iconArrayIndex = 2;}
                else if(string.Equals(iconID, "04"))
                    {iconArrayIndex = 3;}
                else if(string.Equals(iconID, "09"))
                    {iconArrayIndex = 4;}
                else if(string.Equals(iconID, "10"))
                    {iconArrayIndex = 5;}
                else if(string.Equals(iconID, "11"))
                    {iconArrayIndex = 6;}
                else if(string.Equals(iconID, "13"))
                    {iconArrayIndex = 7;}
                else if(string.Equals(iconID, "50"))
                    {iconArrayIndex = 8;}
                else
                    {iconArrayIndex = 0;}

            }
        }
    }
  
    // Update is called once per frame
    void UpdateTime()
    {
    string description = "";

    Sun.gameObject.SetActive(false);
    Cloud.gameObject.SetActive(false);
    Snow.gameObject.SetActive(false);

    if(string.Equals(iconStringArray[iconArrayIndex], "01"))
        {description = "clear sky";Sun.gameObject.SetActive(true);}
    else if(string.Equals(iconStringArray[iconArrayIndex], "02"))
        {description = "few clouds";Cloud.gameObject.SetActive(true);}
    else if(string.Equals(iconStringArray[iconArrayIndex], "03"))
        {description = "scattered clouds";Cloud.gameObject.SetActive(true);}
    else if(string.Equals(iconStringArray[iconArrayIndex], "04"))
        {description = "broken clouds";Cloud.gameObject.SetActive(true);}
    else if(string.Equals(iconStringArray[iconArrayIndex], "09"))
        {description = "shower rain";Cloud.gameObject.SetActive(true);}
    else if(string.Equals(iconStringArray[iconArrayIndex], "10"))
        {description = "rain";Cloud.gameObject.SetActive(true);}
    else if(string.Equals(iconStringArray[iconArrayIndex], "11"))
        {description = "thunderstorm";Cloud.gameObject.SetActive(true);}
    else if(string.Equals(iconStringArray[iconArrayIndex], "13"))
        {description = "snow";Snow.gameObject.SetActive(true);}
    else if(string.Equals(iconStringArray[iconArrayIndex], "50"))
        {description = "mist";Cloud.gameObject.SetActive(true);}
    else
        {description = "Error";}

    iconTextObject.GetComponent<TextMeshPro>().text = description;
    
    }
}