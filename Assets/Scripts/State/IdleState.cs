using ManaMist.Input;
using ManaMist.Models;
using UnityEngine;

namespace ManaMist.State
{
    [CreateAssetMenu(menuName = "ManaMist/States/IdleState")]
    public class IdleState : GameState
    {

        public override void HandleInput(InputEvent inputEvent)
        {
            if (inputEvent is MapTileClickedInput)
            {
                MapTileClickedInput mapTileClickedInput = inputEvent as MapTileClickedInput;

                MapTile mapTile = mapController.GetMapTileAtCoordinate(mapTileClickedInput.coordinate);

                if (mapTile.entities.Count > 0 && player.GetEntity(mapTile.entities[0].id) != null)
                {
                    SelectedStateData selectedStateData = ScriptableObject.CreateInstance<SelectedStateData>();
                    selectedStateData.coordinate = mapTileClickedInput.coordinate;

                    dispatcher.Dispatch<SelectedState>(selectedStateData);
                }
            }
            else if (inputEvent is EndOfTurnInput)
            {
                dispatcher.Dispatch<WaitingState>();
            }
        }

        protected override void Enter()
        {
        }

        protected override void Exit()
        {
        }
    }
}