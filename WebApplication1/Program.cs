using System.Text;
using Telegram.Bot;
using UtilityBotSF.Controllers;
using UtilityBotSF.Services;
using UtilityBotSF.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace UtilityBotSF
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime() 
                .Build();

            Console.WriteLine("Бот готов к работе");

            await host.RunAsync();
            Console.WriteLine("Бот остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddSingleton<IStorage, MemoryStorage>();

        
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                DownloadsFolder = "",
                BotToken = "5932801548:AAH4uvDzdMYHAl9aJvb3iDWqNjsIx0VVoG0",
            };
        }
    }
}

