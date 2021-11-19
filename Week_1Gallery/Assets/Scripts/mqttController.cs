using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mqttController : MonoBehaviour
{
    public string nameController = "Controller 1";
    public string tagOfTheMQTTReceiver = "";

    public TextMeshProUGUI value_Temperature;
    public TextMeshProUGUI value_SolarRadiation;
    public TextMeshProUGUI value_WindSpeed;
    public TextMeshProUGUI value_Humidity;


    public mqttReceiver _eventSender;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag(tagOfTheMQTTReceiver).Length > 0)
        {
            _eventSender = GameObject.FindGameObjectsWithTag(tagOfTheMQTTReceiver)[0].gameObject.GetComponent<mqttReceiver>();
        }
        else
        {
            Debug.LogError("At least one GameObject with mqttReceiver component and Tag == tagOfTheMQTTReceiver needs to be provided");
        }
        _eventSender.OnMessageArrived += OnMessageArrivedHandler;
    }

    private void OnMessageArrivedHandler(string newMsg)
    {
        var weatherJson = JsonUtility.FromJson<Root>(newMsg);

        Debug.Log(weatherJson.outTemp_C);

        
    //value_Temperature.text=weatherJson.outTemp_C.Substring(0,4)+" C";
    value_Temperature.text=System.Math.Round(float.Parse(weatherJson.outTemp_C),2).ToString()+" C";

    value_SolarRadiation.text=weatherJson.radiation_Wpm2+" Wm<sup>2</sup>";
    value_WindSpeed.text=System.Math.Round(float.Parse(weatherJson.windSpeed_kph),2).ToString()+" kph";
    value_Humidity.text=weatherJson.outHumidity+" %";
        
        //    value_texts.Find(e=>e.name=="value_Temperature").GetComponent<TextMeshProUGUI>().text= newMsg.Substring(0,4)+" C";

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    [System.Serializable]
    public class Root
    {
        public string dateTime ;
        public string barometer_mbar;
        public string inTemp_C;
        public string inHumidity;
        public string outTemp_C;
        public string windSpeed_kph;
        public string windSpeed10_kph;
        public string windDir;
        public string outHumidity;
        public string rainRate_cm_per_hour;
        public string UV;
        public string radiation_Wpm2;
        public string stormRain_cm;
        public string dayRain_cm;
        public string monthRain_cm;
        public string yearRain_cm;
        public string dayET;
        public string monthET;
        public string yearET;
        public string leafWet4;
        public string insideAlarm;
        public string rainAlarm;
        public string outsideAlarm1;
        public string outsideAlarm2;
        public string extraAlarm1;
        public string extraAlarm2;
        public string extraAlarm3;
        public string extraAlarm4;
        public string extraAlarm5;
        public string extraAlarm6;
        public string extraAlarm7;
        public string extraAlarm8;
        public string soilLeafAlarm1;
        public string soilLeafAlarm2;
        public string soilLeafAlarm3;
        public string soilLeafAlarm4;
        public string txBatteryStatus;
        public string consBatteryVoltage_volt;
        public string forecastIcon;
        public string forecastRule;
        public string sunrise;
        public string sunset;
        public string rain_cm;
        public string windGust_kph;
        public string windGustDir;
        public string pressure_mbar;
        public string altimeter_mbar;
        public string appTemp_C;
        public string cloudbase_meter;
        public string dewpoint_C;
        public string heatindex_C;
        public string humidex_C;
        public string inDewpoint_C;
        public string maxSolarRad_Wpm2;
        public string windchill_C;
        public string hourRain_cm;
        public string rain24_cm;
        public string usUnits;
        public string stormStart;
    }





}