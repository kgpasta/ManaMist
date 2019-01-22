using ManaMist.Controllers;
using ManaMist.Input;
using UnityEngine;

namespace ManaMist.State
{
    [CreateAssetMenu(menuName = "ManaMist/States/WaitingState")]
    public class WaitingState : GameState
    {
        private void OnEnable()
        {
            turnController.OnTurnStart += OnTurnStart;
        }

        private void OnDisable()
        {
            turnController.OnTurnStart += OnTurnStart;
        }

        private void OnTurnStart(object sender, TurnEventArgs args)
        {
            dispatcher.Dispatch<IdleState>();
        }

        protected override void Enter()
        {
            turnController.MoveToNextPlayer();
        }

        protected override void Exit()
        {
        }

        public override void HandleInput(InputEvent inputEvent)
        {
            return;
        }
    }

    public class WaitingStateData : GameStateData { }
}