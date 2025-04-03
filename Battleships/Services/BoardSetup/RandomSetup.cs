using Battleships.Extensions;
using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.BoardSetup.Interfaces;
using Battleships.Services.Exceptions;
using Battleships.Services.Factories.Interfaces;

namespace Battleships.Services.BoardSetup
{
    public class RandomSetup(IBoardTemplateFactory boardFactory, IFleetFactory fleetFactory) : IBoardSetupService
    {
        private readonly List<Ship> _ships = [];

        public Board SetupBoard()
        {
            var fleet = fleetFactory.GetFleet();
            var template = boardFactory.GetBoardTemplate();

            if (fleet.Any(layout => !CanPlaceShip(template, layout)))
                throw new CannotPlaceShipException();

            return new Board(template, _ships);
        }

        private bool CanPlaceShip(BoardTemplate template, ShipLayout layout)
        {
            var freeSpaces = template.Locations.Except(_ships.Locations());

            foreach (var point in freeSpaces.OrderBy(_ => Guid.NewGuid()))
            foreach (var orientation in RandomOrientations)
            {
                var ship = new Ship(layout, point, orientation);

                if (!template.Fits(ship)
                    || ship.CoOrdinates.Any(_ships.Locations().Contains)) 
                    continue;

                _ships.Add(ship);
                return true;
            }

            return false;
        }

        private static IEnumerable<Orientation> RandomOrientations => Enum
            .GetValues(typeof(Orientation))
            .Cast<Orientation>()
            .OrderBy(_ => Guid.NewGuid());
    }
}
