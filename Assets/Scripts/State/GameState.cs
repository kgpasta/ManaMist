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

        public abstract void HandleInput();

        public abstract void Update();
    }

    public abstract class GameStateData : ScriptableObject { }
}