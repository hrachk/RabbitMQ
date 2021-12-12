using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Tiarm.RabbitMQ.Consumer
{
    public static class QueueConsumer
    {
        public static string Consume(IModel channel)
        {
            channel.QueueDeclare("demo-queue",
               durable: true,
               exclusive: false,
               autoDelete: false,
               arguments: null
               );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                MessageModel.Message = Encoding.UTF8.GetString(body) ;
                Console.WriteLine(MessageModel.Message);
            };

            channel.BasicConsume("demo-queue", true, consumer);
            Console.WriteLine("Consumer started");

            return MessageModel.Message;
           // Console.ReadLine();
        }
    }
}
