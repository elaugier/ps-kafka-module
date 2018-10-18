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

        [Parameter(Mandatory = false)]
        public string BootstrapServers { get; set; } = "localhost:9092";

        protected override void ProcessRecord()
        {
            var config = new List<KeyValuePair<String, Object>>();
            config.Add(new KeyValuePair<string, object>("BoostrapServers", BootstrapServers));
            var KafkaProducer = new Producer(config: config);
            WriteObject(Message);
        }
    }
}
