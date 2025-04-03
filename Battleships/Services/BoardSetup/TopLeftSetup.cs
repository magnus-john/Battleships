using Battleships.Extensions;
using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.BoardSetup.Interfaces;
using Battleships.Services.Exceptions;
using Battleships.Services.Factories.Interfaces;

namespace Battleships.Services.BoardSetup
{
    public class TopLeftSetup(IBoardTemplateFactory boardFactory, IFleetFactory fleetFactory) : IBoardSetupService
    {
        public Board SetupBoard()
        {
            var fleet = fleetFactory.GetFleet();
            var template = boardFactory.GetBoardTemplate();
            var ships = new List<Ship>();

            foreach (var (layout, y) in fleet.Select((x, i) => (x, i)))
            {
                var ship = new Ship(layout, new Point(0, y), Orientation.Horizontal);

                if (!template.Fits(ship)
                    || ship.CoOrdinates.Any(ships.Locations().Contains))
                    throw new CannotPlaceShipException();

                ships.Add(ship);
            }

            return new Board(template, ships);
        }
    }
}
