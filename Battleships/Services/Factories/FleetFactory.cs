using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Factories.Interfaces;

namespace Battleships.Services.Factories
{
    public class FleetFactory : IFleetFactory
    {
        public Fleet GetFleet(FleetType fleetType) => fleetType switch
        {
            FleetType.Flotilla => new Flotilla(),
            FleetType.Squadron => new Squadron(),
            FleetType.Armada => new Armada(),
            _ => throw new ArgumentOutOfRangeException(nameof(fleetType))
        };
    }
}
