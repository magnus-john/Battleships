using Battleships.Model;
using Battleships.Model.Enums;

namespace Battleships.Services.Factories.Interfaces
{
    public interface IFleetFactory
    {
        Fleet GetFleet(FleetType fleetType);
    }
}
