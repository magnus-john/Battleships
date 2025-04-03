using Battleships.Model.Enums;

namespace Battleships.Services.Interfaces
{
    public interface IConfigService
    {
        BoardSize BoardSize { get; }
        FleetType FleetType { get; }
        SetupType SetupType { get; }
        bool AllowCheating { get; }
    }
}
