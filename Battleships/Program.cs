using Battleships.Services;
using Battleships.Services.Factories;
using Battleships.Services.Factories.Interfaces;
using Battleships.Services.Interfaces;
using Battleships.UI;
using Battleships.UI.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships
{
    internal class Program
    {
        private static void Main(string[] _)
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            var game = services
                .BuildServiceProvider()
                .GetService<IGameService>();

            if (game == null)
                throw new ApplicationException("Unable to start game");

            game.Play();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IConfigManager, AppSettingsConfigManager>()
                .AddSingleton<IConfigService, ConfigService>()
                .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
                .AddSingleton<IBoardSetupFactory, BoardSetupFactory>()
                .AddSingleton<IBoardTemplateFactory, BoardTemplateFactory>()
                .AddSingleton<IFleetFactory, FleetFactory>();

            services
                .AddScoped<IBoardDisplayService, BoardDisplayService>()
                .AddScoped<IGameInterface, ConsoleInterface>()
                .AddScoped<IGameService, SinglePlayerGameService>()
                .AddScoped<IMoveService, MoveService>()
                .AddScoped<ITurnService, PlayerTurnService>();
        }
    }
}
