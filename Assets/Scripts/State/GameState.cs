using ManaMist.Controllers;
using ManaMist.Input;
using ManaMist.Players;
using System;
using UnityEngine;

namespace ManaMist.State
{
    public abstract class GameState : ScriptableObject
    {
        public Dispatcher dispatcher;
        public TurnController turnController;
        public MapController mapController;
        public ResearchController researchController;
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
        protected abstract void Enter();
        public void EnterBase()
        {
            Enter();
            OnEnter?.Invoke(this, EventArgs.Empty);
        }
        protected abstract void Exit();
        public void ExitBase()
        {
            Exit();
            OnExit?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnEnter;
        public event EventHandler OnExit;
    }

    public abstract class GameStateData : ScriptableObject { }
}