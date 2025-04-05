using Battleships.Model.Enums;
using Battleships.Services.BoardSetup.Interfaces;

namespace Battleships.Services.Factories.Interfaces
{
    public interface IBoardSetupFactory
    {
        IBoardSetupService GetSetupService(SetupType setupType);
    }
}
