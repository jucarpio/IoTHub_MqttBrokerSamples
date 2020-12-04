# Azure IoT Hub MQTT broker preview - Device operations sample

CRUD operations for devices can be executed using registry manager available on IoT Hub SDK. With the SDK customers can create, retrieve, update and delete devices, as well as generate SAS keys for devices used to authenticate with the MQTT Broker.

For simplicity, we have provided a .NET project on this repository that allow you to Get or Create device and will provide you the list of created devices as well as SAS token that can be used to authenticate the device with the broker.

## Instructions

We are providing an executable that can be found on:

`IoTHub_MqttBrokerSamples\src\DeviceOperations\DeviceOperations\CreateDevice\bin\Debug\netcoreapp3.1\GetOrCreateDevice.exe`

The exe can be run from the console to create or retrieve a number of devices and their SAS key used to authenticate with the broker. To use it run following command

```sh
.\GetOrCreateDevice.exe "<IoT Hub Connection string>" <Number of devices> <alias>
```

Specified number of device will be created or retrieved if they already exist and displayed to the user alongside their connection string.

Output will look like following:

> Device already exists
> . Device id: alias-device-0
> . SAS Token: Token

> Device already exists
> . Device id: alias-device-1
> . SAS Token: Token
