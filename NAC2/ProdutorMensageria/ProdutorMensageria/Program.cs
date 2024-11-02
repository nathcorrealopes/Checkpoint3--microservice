using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ProdutorMensageria
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configuração de conexão com o RabbitMQ
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declaração da fila
                channel.QueueDeclare(queue: "produto_cadastrado",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Dados do produto
                var produto = new
                {
                    produtoId = "123",
                    nomeProduto = "Teclado Gamer",
                    quantidadeInicial = 10
                };

                // Serialização do produto para JSON
                var message = JsonConvert.SerializeObject(produto);
                var body = Encoding.UTF8.GetBytes(message);

                // Publicação da mensagem na fila
                channel.BasicPublish(exchange: "",
                                     routingKey: "produto_cadastrado",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Produto cadastrado enviado: {0}", message);
            }

            Console.WriteLine(" Pressione [enter] para sair.");
            Console.ReadLine();
        }
    }
}
