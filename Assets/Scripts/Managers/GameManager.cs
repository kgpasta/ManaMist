using System;
using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.State;
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
        public EntityController entityController;

        [Header("State")]
        public Dispatcher dispatcher;
        public GameState state;

        private const string resourceMapPath = "Maps/";

        private void Start()
        {
            SetupGame();
        }

        private void OnEnable()
        {
            dispatcher.OnDispatch += SetState;
            turnController.OnTurnStart += OnTurnStart;
        }

        private void OnDisable()
        {
            dispatcher.OnDispatch -= SetState;
            turnController.OnTurnStart -= OnTurnStart;
        }

        private void SetState(object sender, GameState gameState)
        {
            state = gameState;
            state.Update();
        }

        private void SetupGame()
        {
            // Setup Map
            mapController.SetupMap(resourceMapPath + mapName);

            int i = 0;
            // Initialize Players
            foreach (Player player in players)
            {
                i++;
                SeedPlayer(player, i * 10); // NOTE: This is a random temporary seeding offset
            }

            // Initialize Turn Controller
            turnController.Init(players);
        }

        private void OnTurnStart(object sender, TurnEventArgs args)
        {
            Player currentPlayer = players.Find(player => player.id == args.player.id);
            currentPlayer?.InitializeTurn();
        }

        private void SeedPlayer(Player player, int offset)
        {
            Coordinate townCenterCoordinate = new Coordinate(offset, offset);
            Entity townCenter = entityController.CreateEntity(EntityType.TownCenter);
            player.AddEntity(townCenter);
            mapController.AddToMap(townCenterCoordinate, townCenter);

            Coordinate workerCoordinate = new Coordinate(offset + 1, offset + 1);
            Entity worker = entityController.CreateEntity(EntityType.Worker);
            player.AddEntity(worker);
            mapController.AddToMap(workerCoordinate, worker);
        }
    }
}