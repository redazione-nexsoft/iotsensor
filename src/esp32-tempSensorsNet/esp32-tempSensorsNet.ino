/**
   ESP32 + DHT22 + WiFi connection + HTTP GET Sample for Nexsoft
*/
#include <WiFi.h>
#include <HTTPClient.h>
#include "DHTesp.h"

const int MEASURE_INTERVAL_MILLISECONDS = 30 * 1000;

const char* const SSID = "Wokwi-GUEST";
const char* const PASSPHRASE = "";
const char* const URL = "http://redazione.nexsoft.it:9091/deviceprobes"; // API ingress


const int DHT_PIN = 15;


DHTesp dhtSensor;
String SENSORID = "SENSOR_123";

void setup() {
  Serial.begin(115200);

  Serial.print("Activating DHT22 sensor on pin: ");
  Serial.println(DHT_PIN);
  dhtSensor.setup(DHT_PIN, DHTesp::DHT22);

  Serial.print("Connecting to Wi-Fi network: ");
  Serial.println(SSID);
  WiFi.begin(SSID, PASSPHRASE, 6);
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("WiFi connected");
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());
}

void loop() {
  Serial.println("");
  Serial.println("Reading data from sensor:");
  TempAndHumidity  data = dhtSensor.getTempAndHumidity();
  Serial.println("Temp: " + String(data.temperature, 2) + "°C");
  Serial.println("Humidity: " + String(data.humidity, 1) + "%");
  Serial.println("---");
  
  Serial.println("");
  Serial.println(String("Connecting to ") + String(URL));
  HTTPClient http;
  http.setTimeout(15000);
     
  // Forming a secure connection with the server before making the request.
  while(!http.begin(URL)) {
    delay(1000);
    Serial.print(".");
  }

  // Forming the request (the hardest part).
  String request = 
             String("{") 
                  + " \"sensorid\": \"" + String(SENSORID) + "\"," 
                  + " \"measures\":" 
                  + " [" 
                  + "   { \"type\": \"temperature\", \"value\": " + String(data.temperature, 2) + "}," 
                  + "   { \"type\": \"humidity\", \"value\": " + String(data.humidity, 2) + "}" 
                  + " ]" 
                  + "}";

  Serial.println("");
  Serial.println("Posting request:");
  Serial.println(request);

  http.addHeader("Content-Type", "application/json");
  int httpResponseCode = http.POST(request);
  Serial.println("Getting response");

  if (httpResponseCode > 0) {
    Serial.print("HTTP OK: ");
    Serial.println(httpResponseCode);
  }
  else {
    Serial.print("HTTP ERROR: ");
    Serial.println(httpResponseCode);
  }
  
  delay(MEASURE_INTERVAL_MILLISECONDS);
}