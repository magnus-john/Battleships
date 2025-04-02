using Battleships.Model.Enums;

namespace Battleships.Services.Interfaces
{
    public interface IConfigService
    {
        BoardSize BoardSize { get; }
        FleetType FleetType { get; }
        SetupType SetupType { get; }

        int Small { get; }
        int Medium { get; }
        int Large { get; }

        bool AllowCheating { get; }
    }
}
