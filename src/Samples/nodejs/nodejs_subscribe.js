var mqtt = require('mqtt');
const fs = require('fs');

var iot_hub_hostname = "<IotHubHostName>";
var device_id = "<DeviceId>";
var sas_key = "<SasKey>";
var ca_cert = "<Path_to_cert>";

var caFile = fs.readFileSync(ca_cert);

var options={
    clientId: device_id,
    port:8883,
    protocol:'mqtts',
    rejectUnauthorized : false,
    ca: caFile,
    username: iot_hub_hostname + "/" + device_id + "/api-version=2019-06-30",
    password: sas_key,
}

console.log("Connecting to MQTT broker");
var client  = mqtt.connect("mqtts://" + iot_hub_hostname, options);

client.on("connect",function(){	
    console.log("connected  "+ client.connected);
})
    
client.subscribe("<topic>",{qos:1});
console.log("Subscribe to topic. Waiting for messages to be delivered");

client.on('message', function(topic, message, packet){
	console.log("Received Mqtt message. Topic: "+ topic + ". Content: " + message);
});