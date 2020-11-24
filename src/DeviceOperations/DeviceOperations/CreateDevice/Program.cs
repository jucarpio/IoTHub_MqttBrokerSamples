using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Security;

namespace CreateDevice
{
    class Program
    {
        static string IotHubConnectionString = "HostName=free-preview-01.azure-devices-int.net;SharedAccessKeyName=iothubowner;SharedAccessKey=sufVRQzUqayAMo/rxjYogqMGjjYCxXXfy5yftnBRCj4=";
        static string IotHubHostName = "free-preview-01.azure-devices-int.net";

        static async Task Main(string[] args)
        {
            Console.WriteLine($"Get or create devices for IoT Hub {IotHubHostName}");
            await GetOrCreateDevice("Device01");
            await GetOrCreateDevice("Device02");
            //await GetOrCreateDevice("Paho-01");
            //await GetOrCreateDevice("Mosquitto-01");
        }

        static async Task GetOrCreateDevice(string deviceId)
        {
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(IotHubConnectionString);

            var device = await registryManager.GetDeviceAsync(deviceId);
            if (device != null)
            {
                var sasKeyBuilder = new SharedAccessSignatureBuilder
                {
                    Key = device.Authentication.SymmetricKey.PrimaryKey,
                    TimeToLive = TimeSpan.FromDays(1),
                    Target = IotHubHostName + "/devices/" + WebUtility.UrlEncode(device.Id)
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
                Target = IotHubHostName + "/devices/" + WebUtility.UrlEncode(device.Id)
            };
            Console.WriteLine($"Device created\n. Device id: {deviceId}\n. SAS Token: {builder.ToSignature()}");
        }
    }
}
