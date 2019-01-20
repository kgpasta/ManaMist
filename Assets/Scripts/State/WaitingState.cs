using ManaMist.Input;
using UnityEngine;

namespace ManaMist.State
{
    [CreateAssetMenu(menuName = "ManaMist/States/WaitingState")]
    public class WaitingState : GameState
    {
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