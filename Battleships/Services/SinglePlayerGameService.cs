using Battleships.Model;
using Battleships.Services.Factories.Interfaces;
using Battleships.Services.Interfaces;
using Battleships.UI.Interfaces;

namespace Battleships.Services
{
    public class SinglePlayerGameService(
        IBoardSetupFactory setupFactory, 
        IConfigService config,
        IGameInterface ui, 
        ITurnService turnService) : IGameService
    {
        private readonly bool _allowCheating = config.AllowCheating;

        public void Play()
        {
            var setupService = setupFactory.GetSetupService(config.SetupType);
            var board = setupService.SetupBoard(config.BoardSize, config.FleetType);
            var response = turnService.Initialise(board);

            while (true)
            {
                UpdateDisplay(response);

                if (response.GameIsOver)
                    break;

                var move = turnService.GetMove();

                if (move.IsExit)
                    break;

                response = turnService.ProcessMove(response.Board, move);
            }
        }

        private void UpdateDisplay(Response response)
        {
            ui.Display(response.Board, _allowCheating);
            ui.Display(response.Result);

            if (response.GameIsOver)
                ui.DisplayWinMessage();
        }
    }
}
