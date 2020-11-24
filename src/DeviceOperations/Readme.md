# Azure IoT Hub MQTT broker preview - Device operations sample

Crud operations for devices can be executed using registry manager available on IoT Hub SDK. With the SDK customers can create, retrieve, update and delete devices, as well as generate SAS keys for devices used to authenticate with the MQTT Broker.

For simplicity, we have provided a dot net project on this repository that allow you to Get or Create device and will provide you the list of created devices as well as SaS key that can be used to authenticate the device with the broker.

## Instructions

-	Go to src\DeviceOperations\DeviceOperations.sln solution and open Program.cs file. 
-	Change IoTHubConnectionString and IoTHubHostName for the values for your IoTHub
-	On the main function call:
    
    ```csharp
    await GetOrCreateDevice("DeviceID"); 
    ```

	For every device that you want to create 

-	The program will output a list of devices and their SAS keys that can be used to use the IoT Hub MQTT broker.