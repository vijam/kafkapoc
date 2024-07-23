using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kafaka
{
    public class KafkaSettings
    {
        public ProducerConfig Producer { get; set; }
        public ConsumerConfig Consumer { get; set; }
    }
}
