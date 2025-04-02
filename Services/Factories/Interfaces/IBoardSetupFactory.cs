using Battleships.Services.BoardSetup.Interfaces;

namespace Battleships.Services.Factories.Interfaces
{
    public interface IBoardSetupFactory
    {
        IBoardSetupService GetSetupService();
    }
}
