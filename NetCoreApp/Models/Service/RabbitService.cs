using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NetCoreApp.Models.Service
{
	public class RabbitService : IRabbitService
	{
		private const string QUEUE = "cellery";

		private readonly ConnectionFactory _connectionFactory = new ConnectionFactory
		{
			UserName = "guest",
			Password = "guest",
			VirtualHost = "/",
			HostName = "localhost",
			Port = 5672,
		};
		
		public void Send(byte[] file_bytes)
		{
			using(var connection = _connectionFactory.CreateConnection())
			using(var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: QUEUE,
					durable: true,
					exclusive: false,
					autoDelete: false,
					arguments: null);

				channel.BasicPublish(exchange: "",
					routingKey: QUEUE,
					basicProperties: null,
					body: file_bytes);
			}
		}

		public void Receive()
		{
			using(var connection = _connectionFactory.CreateConnection())
			using(var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: QUEUE,
					durable: true,
					exclusive: false,
					autoDelete: false,
					arguments: null);

				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += (model, ea) =>
				{
					var body = ea.Body;
					var message = Encoding.UTF8.GetString(body);
					Console.WriteLine($"Received {message}");
				};
				channel.BasicConsume(queue: QUEUE,
					autoAck: true,
					consumer: consumer);
				
				connection.Close();
			}
		}
	}
}