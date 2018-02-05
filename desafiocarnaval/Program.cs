using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace desafiocarnaval
{
    public class Program
    {
	    public static IConnection _connection;
	    public static IModel _model;
	    public static IBasicProperties _basicProperties;

        public static void Main(string[] args)
        {
	        ConfiguraRabbit();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

	    public static void ConfiguraRabbit()
	    {
		    ConnectionFactory connectionFactory = new ConnectionFactory();
		    connectionFactory.HostName = "localhost";
		    connectionFactory.UserName = "guest";
		    connectionFactory.Password = "guest";
 
		    _connection = connectionFactory.CreateConnection();

		    _model = _connection.CreateModel();  

		    _model.QueueDeclare("queueDesafio", true, false, false, null);  

		    _model.ExchangeDeclare("exchangeDesafio", ExchangeType.Topic, true);

		    _model.QueueBind("queueDesafio", "exchangeDesafio", "desafio");

		    _basicProperties = _model.CreateBasicProperties();
		    _basicProperties.SetPersistent(true);
		
	
	    }
    }
}
