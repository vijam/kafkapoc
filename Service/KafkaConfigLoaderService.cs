using Confluent.Kafka;
using kafaka;
using System;
using System.Collections.Specialized;
using System.Configuration;

namespace kafka
{
    public static class KafkaConfigLoader
    {
        private static NameValueCollection _kafkaConfiguration;

        public static KafkaSettings LoadSettings()
        {
            _kafkaConfiguration = ConfigurationManager.GetSection("KafkaSettings") as NameValueCollection;

            string GetAppSetting(string key)
            {
                var value = _kafkaConfiguration[key];
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value), $"AppSetting '{key}' is missing or empty.");
                }
                return value;
            }

            return new KafkaSettings
            {
                Producer = new ProducerConfig
                {
                    BootstrapServers = GetAppSetting("Producer.BootstrapServers"),
                    SecurityProtocol = (SecurityProtocol)Enum.Parse(typeof(SecurityProtocol), GetAppSetting("Producer.SecurityProtocol")),
                    SaslMechanism = (SaslMechanism)Enum.Parse(typeof(SaslMechanism), GetAppSetting("Producer.SaslMechanism")),
                    SaslUsername = GetAppSetting("Producer.SaslUsername"),
                    SaslPassword = GetAppSetting("Producer.SaslPassword"),
                    Debug = GetAppSetting("Producer.Debug")
                },
                Consumer = new ConsumerConfig
                {
                    BootstrapServers = GetAppSetting("Consumer.BootstrapServers"),
                    SecurityProtocol = (SecurityProtocol)Enum.Parse(typeof(SecurityProtocol), GetAppSetting("Consumer.SecurityProtocol")),
                    SaslMechanism = (SaslMechanism)Enum.Parse(typeof(SaslMechanism), GetAppSetting("Consumer.SaslMechanism")),
                    SaslUsername = GetAppSetting("Consumer.SaslUsername"),
                    SaslPassword = GetAppSetting("Consumer.SaslPassword"),
                    GroupId = GetAppSetting("Consumer.GroupId"),
                    AutoOffsetReset = (AutoOffsetReset)Enum.Parse(typeof(AutoOffsetReset), GetAppSetting("Consumer.AutoOffsetReset")),
                    Debug = GetAppSetting("Consumer.Debug")
                }
            };
        }
    }
}
