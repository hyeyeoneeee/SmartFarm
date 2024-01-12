using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class WeatherController : MonoBehaviour
{
    public string APP_ID;
    public TextMeshProUGUI weatherText;
    public RawImage weatherIcon;
    public WeatherData weatherInfo;

    // Start is called before the first frame update
    void Start()
    {
        CheckCityWeather("Seoul");
    }

    public void CheckCityWeather(string city)
    {
        StartCoroutine(GetWeather(city));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GetWeather(string city)
    {
        city = UnityWebRequest.EscapeURL(city);
        string url = "http://api.openweathermap.org/data/2.5/weather?q=Seoul&units=metric&appid=10b9dc9fd5cb8ee4eb932a9d88fb2ebe";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string json = www.downloadHandler.text;
        json = json.Replace("\"base\":", "\"basem\":");
        weatherInfo = JsonUtility.FromJson<WeatherData>(json);
        weatherText.text = weatherInfo.name + "\n"; //도시이름
        weatherText.text += "현재온도 " + weatherInfo.main.temp.ToString("N1") + "°C\n";
        weatherText.text += "습도 " + weatherInfo.main.humidity + "\n";
        weatherText.text += "풍속 " + weatherInfo.wind.speed + "m/s";

    //    StartCoroutine(GetWeatherIcon(weatherInfo.weather[0].icon));
    }

    //IEnumerator GetWeatherIcon(string icon)
    //{
    //    string url = "https://openweathermap.org/img/wn/" + icon + "@2x.png";
    //    UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
    //    yield return www.SendWebRequest();

    //    weatherIcon.gameObject.SetActive(true);
    //    weatherIcon.texture = DownloadHandlerTexture.GetContent(www);
    //}
}
