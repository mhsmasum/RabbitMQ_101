using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace MessageProducer
{
   static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("message-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var messsage = new { Name = "Message Produced", Description = "Rabbit MQ 101 Message Queue" };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messsage));
            channel.BasicPublish("", "message-queue", null, body);
            
        }
    }
}
