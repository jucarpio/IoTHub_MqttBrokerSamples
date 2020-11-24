# Azure IoT Hub MQTT broker preview
## Using MQTT broker with Azure IoT Hub
In this set of samples we will show how to connect and use MQTT Broker on Azure IoT Hub preview as well as perform MQTT operations like: 
  - Connect to the broker with clean sessions using devices associated to IoT Hub
  - Subscribe and receive messages from custom topics 
  - Publish messages to custom topics 
 
### Prerequisites 
To be able to use MQTT broker in private preview, it is required to obtain an IoT Hub connection string 

### Installation
This samples use different tools and oper source libraries, so every sample includes intructions on libraries and tools that are required

### Contents

Below if the file contents of this repository.

| File/folder | Description |
| ------ | ------ |
| src/DeviceOperations | Dotnet solution with sample code on how to perform CRUD operation for devices and get SAS tokens for devices on private preview IoT Hub |
| src/BrokerSamples | Samples on how to perform mqtt broker operations on IoT Hub using different open source tools |
| IoTHubRootCA_Baltimore.pem | Certificate used in these samples |

# Azure IoT Hub MQTT broker preview
## Using MQTT broker with Azure IoT Hub
In this set of samples we will show how to connect and use MQTT Broker on Azure IoT Hub preview as well as perform MQTT operations like: 
  - Connect to the broker with clean sessions using devices associated to IoT Hub
  - Subscribe and receive messages from custom topics 
  - Publish messages to custom topics 
 
### Prerequisites 
To be able to use MQTT broker in private preview, it is required to obtain an IoT Hub connection string 

### Installation
This samples use different tools and oper source libraries, so every sample includes intructions on libraries and tools that are required

### Contents

Below if the file contents of this repository.

| File/folder | Description |
| ------ | ------ |
| src/DeviceOperations | Dotnet solution with sample code on how to perform CRUD operation for devices and get SAS tokens for devices on private preview IoT Hub |
| src/BrokerSamples | Samples on how to perform mqtt broker operations on IoT Hub using different open source tools |
| IoTHubRootCA_Baltimore.pem | Certificate used in these samples |

### MQTT Broker instructions

Every folder has different samples for each language or tool. However, there are some instructions that are generic for all tools that are required to use IoT Hub MQTT broker with any tool

- For the **clientId** use the deviceId
- For the **Username** field use `{iothubhostname}/{device_id}/?api-version=2018-06-30` where {iothubhostname} is the full CName of the IoT Hub
- For the **Password** field, use a SAS token. The format of SAS token is `SharedAccessSignature sig={signature-string}&se={expiry}&sr={URL-encoded-resourceURI}`. Access tokens can be generated for devices using sample on src\DeviceOperations.
- For **CA Certificate** use the IoTHubRootCA_Baltimore.pem located at the root of the repo.

### Samples

Tools availables and instructions on how to use them in your own application are linked below.

| Language | README |
| ------ | ------ |
| Dotnet | [src/BrokerSamples/dotnet/README.md](src/BrokerSamples/dotnet/README.md) |
| Python | [src/BrokerSamples/python/README.md](src/BrokerSamples/python/README.md) |
| NodeJs | [src/BrokerSamples/nodejs/README.md](src/BrokerSamples/nodejs/README.md)|
| Mosquitto | [src/BrokerSamples/mosquitto/README.md](src/BrokerSamples/mosquitto/README.md) |

### Sample instructions

For the samples following values need to be changed to make them work with your IoT Hub

- IoTHubHostname: URL of your IoT Hub. Format `iot-hub-name`.azure-devices-int.net
- DeviceId: device that want to connect to IoT hub
- SasKey: sas key for the device
- Path_to_cert: path to client certificate used to connect to IoT Hub

### MQTT Box  

Different UI tools can be used to connect to IoT Hub mqtt broker and publish messages to topic as well as subscribe to different topics. One example is [Mqtt Box](http://workswithweb.com/mqttbox.html)






