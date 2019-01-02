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

        public int numberOfPlayers;

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

            // Initialize Players
            for (int i=0; i < numberOfPlayers; i++)
            {
                Player player = ScriptableObject.CreateInstance<Player>();
                player = new Player(i, turnController);
                SeedPlayer(player, i*10); // NOTE: This is a random temporary seeding offset
                players.Add(player);
            }

            // Initialize Turn Controller
            turnController.Init(numberOfPlayers);
        }

        private void SeedPlayer(Player player, int offset)
        {
            Coordinate townCenterCoordinate = new Coordinate(offset, offset);
            TownCenter townCenter = ScriptableObject.CreateInstance<TownCenter>();
            player.AddEntity(townCenter);
            mapController.AddToMap(townCenterCoordinate, townCenter);

            Coordinate mineCoordinate = new Coordinate(offset + 2, offset + 2);
            Mine mine = ScriptableObject.CreateInstance<Mine>();
            player.AddEntity(mine);
            mapController.AddToMap(mineCoordinate, mine);

            Coordinate workerCoordinate = new Coordinate(offset + 1, offset + 1);
            Worker worker = ScriptableObject.CreateInstance<Worker>();
            player.AddEntity(worker);
            mapController.AddToMap(workerCoordinate, worker);
        }
    }
}