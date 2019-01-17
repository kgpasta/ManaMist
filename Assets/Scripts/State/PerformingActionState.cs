using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.State
{
    public class PerformingActionStateData : GameStateData
    {
        public Action action;
        public Coordinate coordinate;
        public Entity target;
    }

    [CreateAssetMenu(menuName = "ManaMist/States/PerformingActionState")]
    public class PerformingActionState : GameState
    {

        public override void HandleInput()
        {
            return;
        }

        public override void Update()
        {
            PerformingActionStateData performingActionStateData = data as PerformingActionStateData;
            MapTile mapTile = mapController.GetMapTileAtCoordinate(performingActionStateData.coordinate);
            Entity entity = mapTile.entities[0];

            if (performingActionStateData.action != null)
            {
                if (performingActionStateData.action.CanExecute(mapController, player, entity, performingActionStateData.coordinate, performingActionStateData.target))
                {
                    performingActionStateData.action.Execute(mapController, player, entity, performingActionStateData.coordinate, performingActionStateData.target);
                }
            }
        }
    }
}