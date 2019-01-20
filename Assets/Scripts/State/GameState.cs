using ManaMist.Controllers;
using ManaMist.Input;
using ManaMist.Players;
using UnityEngine;

namespace ManaMist.State
{
    public abstract class GameState : ScriptableObject
    {
        public Dispatcher dispatcher;
        public TurnController turnController;
        public MapController mapController;
        public Player player;

        [SerializeField]
        private GameStateData m_Data;
        public GameStateData data
        {
            get { return m_Data; }
            set
            {
                if (m_Data != null)
                {
                    Destroy(m_Data);
                }
                m_Data = value;
            }
        }

        public abstract void HandleInput(InputEvent inputEvent);
        public abstract void Enter();
        public abstract void Exit();
    }

    public abstract class GameStateData : ScriptableObject { }
}