using System;
using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Commands;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Processors;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Managers
{
    public class GameManager : MonoBehaviour
    {
        public string mapName;

        [Header("Players")]
        public Player playerOne;
        public Player playerTwo;
        public Player activePlayer;

        [Header("Controllers")]
        public TurnController turnController;
        public MapController mapController;

        private const string resourceMapPath = "Maps/";

        private void Awake()
        {
            // Setup Map
            mapController.SetupMap(resourceMapPath + mapName);

            //SetupGame();
        }

        private void SetupGame()
        {
            turnController.OnTurnStart += setActivePlayer;

            playerOne = new Player(0, turnController);
            SeedPlayer(playerOne, 0);
            playerTwo = new Player(1, turnController);
            SeedPlayer(playerTwo, 10);
            activePlayer = playerOne;

            turnController.StartTurns();
        }

        private void setActivePlayer(object sender, TurnEventArgs args)
        {
            if (args.player == 1)
            {
                activePlayer = playerOne;
            }
            else
            {
                activePlayer = playerTwo;
            }
        }

        public void DoCommand(Command command)
        {
            switch (command.type)
            {
                case CommandType.DESCRIBE:
                    DescribeCommand describeCommand = (DescribeCommand)command;
                    describeCommand.Execute(mapController, describeCommand.id != null ? FindEntity(describeCommand.id) : null);
                    break;
                case CommandType.SELECT:
                    SelectCommand selectCommand = (SelectCommand)command;
                    selectCommand.Execute(mapController, GetPlayerById(selectCommand.playerId));
                    break;
                case CommandType.PERFORMACTION:
                    PerformActionCommand<MoveAction> performActionCommand = (PerformActionCommand<MoveAction>)command;
                    performActionCommand.Execute(mapController, GetPlayerById(performActionCommand.playerId), GetPlayerById(performActionCommand.playerId).selectedEntity);
                    break;
                case CommandType.ENDTURN:
                    EndTurnCommand endTurnCommand = (EndTurnCommand)command;
                    endTurnCommand.Execute(turnController);
                    break;
                default:
                    break;
            }
        }

        private void SeedPlayer(Player player, int offset)
        {
            Coordinate townCenterCoordinate = new Coordinate(offset, offset);
            TownCenter townCenter = new TownCenter();
            player.AddEntity(townCenter);
            mapController.AddToMap(townCenterCoordinate, townCenter);

            Coordinate mineCoordinate = new Coordinate(offset + 2, offset + 2);
            Mine mine = new Mine();
            player.AddEntity(mine);
            mapController.AddToMap(mineCoordinate, mine);

            Coordinate workerCoordinate = new Coordinate(offset + 1, offset + 1);
            Worker worker = new Worker();
            player.AddEntity(worker);
            mapController.AddToMap(workerCoordinate, worker);
        }

        private Entity FindEntity(string id)
        {
            Entity playerOneEntity = playerOne.GetEntity(id);
            Entity playerTwoEntity = playerTwo.GetEntity(id);

            if (playerOneEntity != null)
            {
                return playerOneEntity;
            }
            else if (playerTwoEntity != null)
            {
                return playerTwoEntity;
            }
            return null;
        }

        private Player GetPlayerById(int playerId)
        {
            return playerId == 1 ? playerOne : playerTwo;
        }
    }
}