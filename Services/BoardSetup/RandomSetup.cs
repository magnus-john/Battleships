using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.BoardSetup.Interfaces;
using Battleships.Services.Exceptions;
using Battleships.Services.Factories.Interfaces;

namespace Battleships.Services.BoardSetup
{
    public class RandomSetup(IBoardFactory boardFactory, IFleetFactory fleetFactory) : IBoardSetupService
    {
        public Board SetupBoard()
        {
            var fleet = fleetFactory.GetFleet();
            var board = boardFactory.GetBoard();

            if (fleet.Any(layout => !CanPlaceShip(board, layout)))
                throw new CannotPlaceShipException();

            return board;
        }

        private static bool CanPlaceShip(Board board, ShipLayout layout)
        {
            foreach (var point in board.FreeSpaces.OrderBy(_ => Guid.NewGuid()))
            foreach (var orientation in GetRandomOrientations())
            {
                if (board.Add(new Ship(layout, point, orientation)))
                    return true;
            }

            return false;
        }

        private static IEnumerable<Orientation> GetRandomOrientations()
        {
            return Enum
                .GetValues(typeof(Orientation))
                .Cast<Orientation>()
                .OrderBy(_ => Guid.NewGuid());
        } 
    }
}
