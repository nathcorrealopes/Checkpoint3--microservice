using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumidorMensageria
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "produto_cadastrado",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var produto = JsonConvert.DeserializeObject<dynamic>(message);

                    Console.WriteLine(" [x] Produto recebido:");
                    Console.WriteLine(" ID: {0}", produto.produtoId);
                    Console.WriteLine(" Nome: {0}", produto.nomeProduto);
                    Console.WriteLine(" Quantidade Inicial: {0}", produto.quantidadeInicial);
                };

                channel.BasicConsume(queue: "produto_cadastrado",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Consumidor esperando mensagens. Pressione [enter] para sair.");
                Console.ReadLine();
            }
        }
    }
}
