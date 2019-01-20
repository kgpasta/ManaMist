using ManaMist.Input;
using UnityEngine;

namespace ManaMist.State
{
    [CreateAssetMenu(menuName = "ManaMist/States/IdleState")]
    public class IdleState : GameState
    {
        public override void Enter()
        {
        }

        public override void Exit()
        {
            return;
        }

        public override void HandleInput(InputEvent inputEvent)
        {
            if (inputEvent is MapTileClickedInput)
            {
                MapTileClickedInput mapTileClickedInput = inputEvent as MapTileClickedInput;

                SelectedStateData selectedStateData = ScriptableObject.CreateInstance<SelectedStateData>();
                selectedStateData.coordinate = mapTileClickedInput.coordinate;

                dispatcher.Dispatch<SelectedState>(selectedStateData);
            }
        }
    }

    public class IdleStateData : GameStateData { }
}