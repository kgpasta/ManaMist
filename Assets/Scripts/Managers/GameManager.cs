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
        public List<Player> players = new List<Player>();

        [Header("Controllers")]
        public TurnController turnController;
        public MapController mapController;
        public CommandController commandController;

        private const string resourceMapPath = "Maps/";

        private void Awake()
        {
            SetupGame();
        }

        private void SetupGame()
        {
            // Setup Map
            mapController.SetupMap(resourceMapPath + mapName);

            int i = 0;
            // Initialize Players
            foreach (Player player in players)
            {
                i += 10;
                SeedPlayer(player, i * 10); // NOTE: This is a random temporary seeding offset
                players.Add(player);
            }

            // Initialize Turn Controller
            turnController.Init(players);

            turnController.OnTurnStart += OnTurnStart;
        }

        private void OnTurnStart(object sender, TurnEventArgs args)
        {
            Player currentPlayer = players.Find(player => player.id == args.player.id);
            currentPlayer?.InitializeTurn();
        }

        private void SeedPlayer(Player player, int offset)
        {
            Coordinate townCenterCoordinate = new Coordinate(offset, offset);
            Building townCenter = ScriptableObject.CreateInstance<Building>();
            player.AddEntity(townCenter);
            mapController.AddToMap(townCenterCoordinate, townCenter);

            Coordinate mineCoordinate = new Coordinate(offset + 2, offset + 2);
            Building mine = ScriptableObject.CreateInstance<Building>();
            player.AddEntity(mine);
            mapController.AddToMap(mineCoordinate, mine);

            Coordinate workerCoordinate = new Coordinate(offset + 1, offset + 1);
            Unit worker = ScriptableObject.CreateInstance<Unit>();
            player.AddEntity(worker);
            mapController.AddToMap(workerCoordinate, worker);
        }
    }
}