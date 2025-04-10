using Battleships.Extensions;
using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.BoardSetup.Interfaces;
using Battleships.Services.Exceptions;
using Battleships.Services.Factories.Interfaces;

namespace Battleships.Services.BoardSetup
{
    public class RandomSetup(
        IBoardTemplateFactory templateFactory, 
        IFleetFactory fleetFactory) : IBoardSetupService
    {
        private readonly List<Ship> _ships = [];

        public Board SetupBoard(BoardSize boardSize, FleetType fleetType)
        {
            var template = templateFactory.GetBoardTemplate(boardSize);
            var fleet = fleetFactory.GetFleet(fleetType);

            if (fleet.Any(layout => !CanPlaceShip(template, layout)))
                throw new CannotPlaceShipException();

            return new Board(template, _ships);
        }

        private bool CanPlaceShip(BoardTemplate template, ShipLayout layout)
        {
            var freeSpaces = template.Locations
                .Except(_ships.Locations())
                .OrderBy(_ => Guid.NewGuid());

            var shipLocations = _ships.Locations().ToHashSet();

            foreach (var point in freeSpaces)
                foreach (var orientation in RandomOrientations)
                {
                    var ship = new Ship(layout, point, orientation);

                    if (!template.Fits(ship)
                        || ship.CoOrdinates.Any(shipLocations.Contains))
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
