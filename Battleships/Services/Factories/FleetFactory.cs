using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Factories.Interfaces;
using Battleships.Services.Interfaces;

namespace Battleships.Services.Factories
{
    public class FleetFactory(IConfigService config) : IFleetFactory
    {
        public Fleet GetFleet() => config.FleetType switch
        {
            FleetType.Flotilla => new Flotilla(),
            FleetType.Squadron => new Squadron(),
            FleetType.Armada => new Armada(),
            _ => throw new ArgumentOutOfRangeException(nameof(config.FleetType))
        };
    }
}
