using System;
using System.Text;
using Common;
using log4net;
using RabbitMQ.Client;

namespace Publisher
{
    public class Publisher : IDisposable
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly ILog _log = LogManager.GetLogger("publisher-log");

        const string ExchangeName = "RabbitMQ-lnl-direct";

        public Publisher() {
            connection = new RabbitMqConfiguration().GetRabbitMqConnection();
            channel = connection.CreateModel();
        }

        public void DeclareExchangeAndPutMessage(string topic, string body)
        {
            channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, true);

            var basicProperties = channel.CreateBasicProperties();
            channel.BasicPublish(ExchangeName, topic, basicProperties, Encoding.UTF8.GetBytes(body));

            _log.Info("Published message - " + topic + " - " + body);
        }

        public void Dispose()
        {
            channel.Dispose();
            connection.Dispose();
        }
    }
}
