from paho.mqtt import client as mqtt
import ssl
import os

iot_hub_hostname = "<IotHubHostName>"
device_id = "<DeviceId>"
sas_key = "<SasKey>"
ca_cert = os.path.abspath("<Path_to_cert>")

def on_connect(client, userdata, flags, rc):
    print("Device connected with result code: " + str(rc))

def on_disconnect(client, userdata, rc):
    print("Device disconnected with result code: " + str(rc))

def on_message(client, userdata, msg):
    print(msg.topic+" "+str(msg.payload))

client = mqtt.Client(client_id=device_id, protocol=mqtt.MQTTv311)

client.on_connect = on_connect
client.on_disconnect = on_disconnect
client.on_message = on_message

username = iot_hub_hostname + "/" + device_id + "/api-version=2019-06-30"

client.username_pw_set(username=username, password=sas_key)
client.tls_set(ca_certs= ca_cert, cert_reqs=ssl.CERT_REQUIRED, tls_version=ssl.PROTOCOL_TLSv1_2, ciphers=None)
client.tls_insecure_set(False)

client.connect(iot_hub_hostname, port=8883)
client.subscribe("<topic>", qos=0)

client.loop_forever()

client.disconnect()