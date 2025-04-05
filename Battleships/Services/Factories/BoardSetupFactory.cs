using Battleships.Model.Enums;
using Battleships.Services.BoardSetup;
using Battleships.Services.BoardSetup.Interfaces;
using Battleships.Services.Factories.Interfaces;

namespace Battleships.Services.Factories
{
    public class BoardSetupFactory(
        IBoardTemplateFactory boardFactory, 
        IFleetFactory fleetFactory) : IBoardSetupFactory
    {
        public IBoardSetupService GetSetupService(SetupType setupType) => setupType switch
        {
            SetupType.Random => new RandomSetup(boardFactory, fleetFactory),
            SetupType.TopLeft => new TopLeftSetup(boardFactory, fleetFactory),
            _ => throw new ArgumentOutOfRangeException(nameof(setupType))
        };
    }
}
