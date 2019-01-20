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

        public override void Enter()
        {
            turnController.MoveToNextPlayer();
        }

        public override void Exit()
        {
            return;
        }

        public override void HandleInput(InputEvent inputEvent)
        {
            return;
        }
    }

    public class WaitingStateData : GameStateData { }
}