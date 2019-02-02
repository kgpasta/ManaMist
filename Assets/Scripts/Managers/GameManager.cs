using System;
using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Input;
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
        public InputController inputController;
        public EntityController entityController;
        public SeedController seedController;

        [Header("State")]
        public Dispatcher dispatcher;
        public GameState state;
        public Dispatcher player2Dispatcher;
        public GameState player2State;

        private const string resourceMapPath = "Maps/";

        private void Start()
        {
            SetupGame();
        }

        private void OnEnable()
        {
            dispatcher.OnDispatch += SetState;
            player2Dispatcher.OnDispatch += SetState;
            turnController.OnTurnStart += OnTurnStart;
            inputController.OnInputEvent += OnInputEvent;
        }

        private void OnDisable()
        {
            dispatcher.OnDispatch -= SetState;
            player2Dispatcher.OnDispatch -= SetState;
            turnController.OnTurnStart -= OnTurnStart;
            inputController.OnInputEvent -= OnInputEvent;
        }

        private void SetState(object sender, GameState newState)
        {
            if (turnController.currentPlayer.id == 0)
            {
                state.ExitBase();
                state = newState;
                state.EnterBase();
            }
            else
            {
                player2State.ExitBase();
                player2State = newState;
                player2State.EnterBase();
            }
        }

        private void OnInputEvent(object sender, InputEvent inputEvent)
        {
            GameState gameState = turnController.currentPlayer.id == 0 ? this.state : this.player2State;
            gameState.HandleInput(inputEvent);
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
                seedController.SeedPlayer(player);
            }

            // Initialize Turn Controller
            turnController.Init(players);
        }

        private void OnTurnStart(object sender, TurnEventArgs args)
        {
            Player currentPlayer = players.Find(player => player.id == args.player.id);
            currentPlayer?.InitializeTurn();
        }
    }
}