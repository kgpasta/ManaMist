using System;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.State
{
    [CreateAssetMenu(menuName = "ManaMist/Dispatcher")]
    public class Dispatcher : ScriptableObject
    {
        public List<GameState> gameStates;
        public event EventHandler<GameState> OnDispatch;
        public void Dispatch<T>(GameStateData data) where T : GameState
        {
            GameState gameState = gameStates.Find(state => state is T);

            if (gameState != null)
            {
                gameState.data = data;
                OnDispatch?.Invoke(this, gameState);
            }
        }
    }
}