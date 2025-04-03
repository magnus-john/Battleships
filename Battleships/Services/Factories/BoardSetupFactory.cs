using Battleships.Model.Enums;
using Battleships.Services.BoardSetup;
using Battleships.Services.BoardSetup.Interfaces;
using Battleships.Services.Factories.Interfaces;
using Battleships.Services.Interfaces;

namespace Battleships.Services.Factories
{
    public class BoardSetupFactory(
        IConfigService config, 
        IBoardTemplateFactory boardFactory, 
        IFleetFactory fleetFactory) : IBoardSetupFactory
    {
        public IBoardSetupService GetSetupService() => config.SetupType switch
        {
            SetupType.Random => new RandomSetup(boardFactory, fleetFactory),
            SetupType.TopLeft => new TopLeftSetup(boardFactory, fleetFactory),
            _ => throw new ArgumentOutOfRangeException(nameof(config.SetupType))
        };
    }
}
