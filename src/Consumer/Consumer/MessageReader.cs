using System;
using System.Text;

using Common;

using log4net;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer_step_1
{
    internal class MessageReader : IDisposable
    {
        private readonly ILog log = LogManager.GetLogger("consumer-log");

        private readonly IConnection connection;
        private readonly IModel channel;
        const string ExchangeName = "RabbitMQ-lnl-direct"; 

        public MessageReader()
        {
            connection = new RabbitMqConfiguration().GetRabbitMqConnection();
            channel = connection.CreateModel();
        }

        public void ConsumeMessages(string routingKey)
        {
            channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, true);
            var queueName = channel.QueueDeclare("consumer_step1_queue", true, false, false, null);
            channel.QueueBind(queueName, ExchangeName, routingKey);
            var consumer = new QueueingBasicConsumer(channel);
            channel.BasicConsume(queueName, false, consumer);

            Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C.");

            while (true)
            {
                var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                var messageContent = Encoding.UTF8.GetString(ea.Body);
                log.Info("Message retrieved:");
                log.Info(messageContent);

                // IMPORTANT! Otherwise the message will be requeued when the connection is closed (when the client dies)
                channel.BasicAck(ea.DeliveryTag, false);
            }
        }

        public void Dispose()
        {
            channel.Dispose();
            connection.Dispose();
        }
    }
}