using System;
using System.Collections;
using System.Collections.Generic;
using ManaMist.Players;
using UnityEngine;

namespace ManaMist.Controllers
{
    public class TurnEventArgs : EventArgs
    {
        public TurnEventArgs(int turnNumber, Player player)
        {
            this.turnNumber = turnNumber;
            this.player = player;
        }
        public Player player { get; set; }
        public int turnNumber { get; set; }
    }

    [CreateAssetMenu(menuName = "ManaMist/Turn Controller")]
    public class TurnController : ScriptableObject
    {
        private Queue<Player> playerQueue = new Queue<Player>();
        public Player currentPlayer;
        [SerializeField] private int m_TurnNumber = 0;
        public int turnNumber
        {
            get
            {
                return m_TurnNumber / playerQueue.Count;
            }
        }
        public event EventHandler<TurnEventArgs> OnTurnStart;
        public event EventHandler<TurnEventArgs> OnTurnEnd;

        private void OnDisable()
        {
            m_TurnNumber = 0;
        }

        public void Init(List<Player> players)
        {
            playerQueue = new Queue<Player>(players);
            currentPlayer = playerQueue.Dequeue();
            OnTurnStart?.Invoke(this, new TurnEventArgs(turnNumber, currentPlayer));
        }

        public void EndTurn()
        {
            OnTurnEnd?.Invoke(this, new TurnEventArgs(turnNumber, currentPlayer));

            MoveToNextPlayer();

            m_TurnNumber++;

            OnTurnStart?.Invoke(this, new TurnEventArgs(turnNumber, currentPlayer));

        }

        private void MoveToNextPlayer()
        {
            playerQueue.Enqueue(currentPlayer);

            currentPlayer = playerQueue.Dequeue();
        }
    }
}
