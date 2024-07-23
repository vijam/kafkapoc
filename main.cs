using kafaka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace kafka
{
    public class MainClass
    {
        private static Producer _producer;
        private static Consumer _consumer;

        public MainClass(Producer producer, Consumer consumer)
        {
            _producer = producer;
            _consumer = consumer;
        }

        static void Main()
        {

            var kafkaSettings = KafkaConfigLoader.LoadSettings();

            _producer = new Producer(kafkaSettings.Producer);
            _consumer = new Consumer(kafkaSettings.Consumer);

            var mainClass = new MainClass(_producer, _consumer);

            var cancellationTokenSource = new CancellationTokenSource();

            // Run the consumer in a separate thread
            var consumerTask = Task.Run(() => _consumer.Consume("vi", cancellationTokenSource.Token));

            // Produce a message
            _producer.ProduceAsync("vi", "key", "value").GetAwaiter().GetResult();

            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancellationTokenSource.Cancel();
            };

            consumerTask.GetAwaiter().GetResult();
        }
    }
}
