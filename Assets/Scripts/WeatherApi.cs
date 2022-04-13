using System.Net;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using Newtonsoft.Json.Linq;


public static class APIHelper
{
    private const string _apiKey = "14430e234ee444ce56573da2b1eaba9d";
    public static string GetTemperature(string city)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={_apiKey}");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (StreamReader reader = new StreamReader(response.GetResponseStream())){
            string json = reader.ReadToEnd();
            var jsonData = (JObject)JsonConvert.DeserializeObject(json);
            return jsonData["main"]["temp"].Value<string>();
        }
    }

}

[RequireComponent(typeof(TMP_Text))]
public class WeatherApi : MonoBehaviour
{
    [SerializeField] private string _city = "Kramatorsk";

    private TMP_Text _temperatureText;

    private void Awake()
    {
        _temperatureText = GetComponent<TMP_Text>();
        float temperature = float.Parse(APIHelper.GetTemperature(_city), System.Globalization.CultureInfo.InvariantCulture);

        int roundTemp = Mathf.RoundToInt(temperature);

        _temperatureText.text = $"Multiplier: {roundTemp} °C";
        PlayerPrefs.SetInt("ScoreMultiplier", roundTemp);
    }


}
