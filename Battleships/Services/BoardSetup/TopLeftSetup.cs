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
        public Board SetupBoard(BoardSize boardSize, FleetType fleetType)
        {
            var boardTemplate = boardFactory.GetBoardTemplate(boardSize);
            var fleet = fleetFactory.GetFleet(fleetType);
            var ships = new List<Ship>();

            foreach (var (layout, y) in fleet.Select((x, i) => (x, i)))
            {
                var ship = new Ship(layout, new Point(0, y), Orientation.Horizontal);

                if (!boardTemplate.Fits(ship)
                    || ship.CoOrdinates.Any(ships.Locations().Contains))
                    throw new CannotPlaceShipException();

                ships.Add(ship);
            }

            return new Board(boardTemplate, ships);
        }
    }
}
