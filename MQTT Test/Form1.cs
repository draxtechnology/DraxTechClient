
using MQTTnet;
using System.Security.AccessControl;
using System.Text;

namespace MQTT_Test
{
    public partial class Form1 : Form
    {
        const string kbroker = "nas";
        const int kport = 1883;
        const string kclientid = "TestClient";
        const string ksendtopic = "test/topicstend";
        const string kreturntopic = "test/topicreturn";
        private IMqttClient client;
        public Form1()
        {
            InitializeComponent();
            startmqtt();
        }

        private void startmqtt()
        {
            // Create a new MQTT client instance
            var mqttfactory = new MqttClientFactory();
            client = mqttfactory.CreateMqttClient();
            // Set up the connection options
            var options = new MQTTnet.MqttClientOptionsBuilder()
                .WithClientId(kclientid)
                .WithTcpServer(kbroker, kport)
                .Build();
            // Connect to the MQTT broker
            recevicehandler();
            client.ConnectAsync(options).GetAwaiter().GetResult();
          

            client.SubscribeAsync(kreturntopic);

            void recevicehandler()
            {
                client.ApplicationMessageReceivedAsync += e =>
                {
                    string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    Console.WriteLine($"Topic: {e.ApplicationMessage.Topic}");

                    Console.WriteLine($"Payload: "+payload);
                    Console.WriteLine($"QoS: {e.ApplicationMessage.QualityOfServiceLevel}");
                    Console.WriteLine($"Retain: {e.ApplicationMessage.Retain}");
                    MessageBox.Show("Received: " + payload, "MQTT Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return Task.CompletedTask;
                };
            }

        }



        private void stopmqtt()
        {
            // Disconnect from the MQTT broker
            client.DisconnectAsync().GetAwaiter().GetResult();
        }
        private void btsend_Click(object sender, EventArgs e)
        {


            // Publish a message to the specified topic
            var message = new MQTTnet.MqttApplicationMessageBuilder()
                .WithTopic(ksendtopic)
                .WithPayload("Hello, From Drax360 " + DateTime.Now.ToLongTimeString())
            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce)
                .Build();

            client.PublishAsync(message).GetAwaiter().GetResult();

            // Disconnect from the broker


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopmqtt();
        }
    }
}
