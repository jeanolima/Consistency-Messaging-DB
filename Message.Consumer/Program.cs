using Message.Consumer.Handler;
using Message.Consumer.Model;
using Message.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using System.Configuration;

namespace Message.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();       
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    using BuiltinHandlerActivator subscriber = new BuiltinHandlerActivator();

                    services.AutoRegisterHandlersFromAssemblyOf<FinalConsumerHandler>();
                    services.AutoRegisterHandlersFromAssemblyOf<MessageConsumerHandler>();
                    services.AddRebus(configure => 
                        configure
                            .Transport(t => t.UseRabbitMq("amqp://guest:guest@localhost:5672", "messagetopic-first"))
                            .Routing(r => r.TypeBased()
                                .MapAssemblyOf<Person>("messagetopic-first"))
                    );

                    services.AddSingleton<IPublisher, Publisher>();

                    var provider = services.BuildServiceProvider();
                    provider.UseRebus();
                });
}
}
