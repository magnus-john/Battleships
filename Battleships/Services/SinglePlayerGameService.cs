﻿using Battleships.Model;
using Battleships.Services.Factories.Interfaces;
using Battleships.Services.Interfaces;

namespace Battleships.Services
{
    public class SinglePlayerGameService(IBoardSetupFactory setupFactory, ITurnService turnService) : IGameService
    {
        public void Play()
        {
            var board = setupFactory.GetSetupService().SetupBoard();
            var result = new MoveResult(board);

            while (true)
            {
                turnService.UpdateDisplay(result);

                if (result.GameIsOver)
                    break;

                var move = turnService.GetMove();

                if (move == null)
                    break;

                result = turnService.ProcessMove(result.Board, move);
            }
        }
    }
}
