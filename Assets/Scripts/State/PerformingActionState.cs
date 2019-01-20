using ManaMist.Actions;
using ManaMist.Input;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.State
{
    public class PerformingActionStateData : GameStateData
    {
        public Action action;
        public Entity source;
        public Coordinate targetCoordinate;
    }

    [CreateAssetMenu(menuName = "ManaMist/States/PerformingActionState")]
    public class PerformingActionState : GameState
    {
        private Action m_Action;
        private Entity m_Source;
        private Coordinate m_TargetCoordinate;

        public override void HandleInput(InputEvent inputEvent)
        {
            if (inputEvent is MapTileClickedInput)
            {
                MapTileClickedInput mapTileClickedInput = inputEvent as MapTileClickedInput;

                m_TargetCoordinate = mapTileClickedInput.coordinate;
                PerformAction();
            }
        }

        public override void Enter()
        {
            PerformingActionStateData performingActionStateData = data as PerformingActionStateData;
            m_Action = performingActionStateData.action;
            m_Source = performingActionStateData.source;
            m_TargetCoordinate = performingActionStateData.targetCoordinate;

            if (m_TargetCoordinate != null)
            {
                PerformAction();
            }
        }

        public override void Exit()
        {
            m_Action = null;
            m_Source = null;
            m_TargetCoordinate = null;
        }

        private void PerformAction()
        {
            if (m_Action.CanExecute(player, m_Source, m_TargetCoordinate))
            {
                m_Action.Execute(player, m_Source, m_TargetCoordinate);
            }

            IdleStateData stateData = ScriptableObject.CreateInstance<IdleStateData>();
            dispatcher.Dispatch<IdleState>(stateData);
        }
    }
}