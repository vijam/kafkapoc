using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace kafka
{
    public class Producer
    {
        private readonly IProducer<string, string> _producer;

        public Producer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, string key, string value)
        {
            try
            {
                var result = await _producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = value });
                Console.WriteLine($"Message sent to {result.TopicPartitionOffset}");
            }
            catch (ProduceException<string, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
