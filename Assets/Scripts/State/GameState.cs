using ManaMist.Controllers;
using ManaMist.Players;
using UnityEngine;

namespace ManaMist.State
{
    public abstract class GameState : ScriptableObject
    {
        public TurnController turnController;
        public MapController mapController;
        public Player player;
        public GameStateData data;

        public abstract void HandleInput();

        public abstract void Update();
    }

    public abstract class GameStateData : ScriptableObject { }
}