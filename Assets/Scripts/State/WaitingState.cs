using UnityEngine;

namespace ManaMist.State
{
    [CreateAssetMenu(menuName = "ManaMist/States/WaitingState")]
    public class WaitingState : GameState
    {
        public override void HandleInput()
        {
            return;
        }

        public override void Update()
        {
            return;
        }
    }

    public class WaitingStateData : GameStateData { }
}