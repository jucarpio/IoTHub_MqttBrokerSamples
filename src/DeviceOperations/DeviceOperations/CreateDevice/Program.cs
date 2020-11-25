using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Security;

namespace CreateDevice
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 3)
            {
                throw new InvalidOperationException("ERROR: expected command input.\\GetOrCreateDevice.exe<IoTHubConnectionString> <IoTHubHostname> <NumberOfDevices> <alias>");
            }

            string iotHubConnectionString = args[0];
            int numberOfDevices = Int32.Parse(args[1]);
            string alias = args[2];
            if (numberOfDevices < 0 || numberOfDevices > 1000)
            {
                throw new InvalidOperationException("ERROR: Device count must be between 0 and 1000");
            }

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] items = iotHubConnectionString.Split(';');
            foreach (string item in items)
            {
                string[] keyValue = item.Split('=');
                dictionary.Add(keyValue[0], keyValue[1]);
            }
            if (!dictionary.ContainsKey("HostName"))
            {
                throw new InvalidOperationException("ERROR: HostName is not present on IoT Hub connection string");
            }

            string iotHubHostName = dictionary["HostName"];

            for (int i = 0; i < numberOfDevices; ++i)
            {
                await GetOrCreateDevice(iotHubConnectionString, iotHubHostName, $"{alias}-device-{i}");
            }
        }

        static async Task GetOrCreateDevice(string iotHubConnectionString, string iotHubHostName, string deviceId)
        {
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(iotHubConnectionString);

            var device = await registryManager.GetDeviceAsync(deviceId);
            if (device != null)
            {
                var sasKeyBuilder = new SharedAccessSignatureBuilder
                {
                    Key = device.Authentication.SymmetricKey.PrimaryKey,
                    TimeToLive = TimeSpan.FromDays(1),
                    Target = iotHubHostName + "/devices/" + WebUtility.UrlEncode(device.Id)
                };

                Console.WriteLine($"Device already exists\n. Device id: {deviceId}\n. SAS Token: {sasKeyBuilder.ToSignature()}");
                return;
            }

            device = new Device(deviceId)
            {
                Authentication = new AuthenticationMechanism
                {
                    SymmetricKey = new SymmetricKey
                    {
                        PrimaryKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString("N"))),
                        SecondaryKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString("N")))
                    }
                }
            };

            await registryManager.AddDeviceAsync(device);

            var builder = new SharedAccessSignatureBuilder
            {
                Key = device.Authentication.SymmetricKey.PrimaryKey,
                TimeToLive = TimeSpan.FromDays(20),
                Target = iotHubHostName + "/devices/" + WebUtility.UrlEncode(device.Id)
            };
            Console.WriteLine($"Device created\n. Device id: {deviceId}\n. SAS Token: {builder.ToSignature()}");
        }
    }
}
