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
            IdleStateData idleStateData = ScriptableObject.CreateInstance<IdleStateData>();
            dispatcher.Dispatch<IdleState>(idleStateData);
        }

        public override void Enter()
        {
            return;
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