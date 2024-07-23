using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace kafka
{
    public class Consumer
    {
        private readonly IConsumer<string, string> _consumer;

        public Consumer(ConsumerConfig config)
        {
            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public void Consume(string topic, CancellationToken cancellationToken)
        {
            _consumer.Subscribe(topic);

            try
            {
                while (true)
                {
                    try
                    {
                        var cr = _consumer.Consume(cancellationToken);
                        Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occurred: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }
        }
    }
}
