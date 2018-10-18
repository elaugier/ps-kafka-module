using System.Management.Automation;
using System.Collections.Generic;
using Confluent.Kafka;
using System;

namespace ps_kafka_module
{
    [Cmdlet(VerbsCommunications.Send, "MessageToKafka")]
    public class SendMessageToKafkaCommand : Cmdlet
    {
        [Parameter(Mandatory =true)]
        public string Message { get; set; }

        [Parameter(Mandatory = true)]
        public string Topic { get; set; }

        [Parameter(Mandatory = false)]
        public string BootstrapServers { get; set; } = "localhost:9092";

        protected override async void ProcessRecord()
        {
            var config = new List<KeyValuePair<String, Object>>();
            config.Add(new KeyValuePair<string, object>("BoostrapServers", BootstrapServers));
            var KafkaProducer = new Producer(config: config);
            try
            {
                var dr = await KafkaProducer.ProduceAsync(Topic, new Message<Null, string> { Value = Message });
                Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
            }
            catch (KafkaException e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
