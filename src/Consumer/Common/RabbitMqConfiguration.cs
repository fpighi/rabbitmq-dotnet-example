using RabbitMQ.Client;

namespace Common
{
    public class RabbitMqConfiguration
    {
        public string HostName = "localhost";
        public string UserName = "guest";
        public string Password = "guest";

        public IConnection GetRabbitMqConnection() {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            return connectionFactory.CreateConnection();
        }
    }
}
