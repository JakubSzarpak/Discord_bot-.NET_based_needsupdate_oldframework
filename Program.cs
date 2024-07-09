using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sugoma
{
    public class Program
    {
        public static Task Main() => new Program().MainAsync();

        public async Task MainAsync()
        {
            using IHost host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) => 
            services
            .AddSingleton(x => new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged,
                AlwaysDownloadUsers = true
            })))
            .Build();

            await RunAsync(host);


        }

        public async Task RunAsync(IHost host)
        {
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            
            var _client = provider.GetRequiredService<DiscordSocketClient>();
            _client.Log += async (LogMessage msg) => { Console.WriteLine(msg.Message); };

            _client.Ready += async () =>
            {
                Console.WriteLine("BOT Gotowy!");
            };

            await _client.LoginAsync(TokenType.Bot, "OTY3MDI1NDc5Mzg5NDIxNjQ4.YmKSqA.hCQMIEe4VZszcthcZlc4b4TNfrA");
            await _client.StartAsync();

            await Task.Delay(-1);

        }

    }

}