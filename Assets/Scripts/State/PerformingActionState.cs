using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.State
{
    public class PerformingActionStateData : GameStateData
    {
        public Action action;
        public Entity source;
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

            if (performingActionStateData.action.CanExecute(player, performingActionStateData.source, performingActionStateData.coordinate, performingActionStateData.target))
            {
                performingActionStateData.action.Execute(player, performingActionStateData.source, performingActionStateData.coordinate, performingActionStateData.target);
            }

        }
    }
}