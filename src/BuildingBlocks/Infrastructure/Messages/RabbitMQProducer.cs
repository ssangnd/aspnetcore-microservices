using Contracts.Common.Interfaces;
using Contracts.Messages;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messages
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly ISerializeService _serializeService;
        public RabbitMQProducer( ISerializeService serializeService)
        {
            _serializeService = serializeService;
        }
        public void SendMessage<T>(T messsage)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("orders",exclusive: false);
            //serilize message to json
            var jsonData = _serializeService.Serialize(messsage);
            var body = Encoding.UTF8.GetBytes(jsonData);
            channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
        }
    }
}
