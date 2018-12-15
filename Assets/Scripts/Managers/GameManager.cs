using System;
using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Commands;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
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

        [Header("Controllers")]
        public TurnController turnController;
        public MapController mapController;
        public CommandController commandController;

        private const string resourceMapPath = "Maps/";

        private void Awake()
        {
            // Setup Map
            mapController.SetupMap(resourceMapPath + mapName);

            //SetupGame();
        }

        private void SetupGame()
        {
            playerOne = new Player(0, turnController);
            SeedPlayer(playerOne, 0);
            playerTwo = new Player(1, turnController);
            SeedPlayer(playerTwo, 10);

            turnController.StartTurns();
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
    }
}