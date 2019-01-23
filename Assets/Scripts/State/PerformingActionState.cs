using ManaMist.Actions;
using ManaMist.Input;
using ManaMist.Models;
using ManaMist.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.State
{
    public class PerformingActionStateData : GameStateData
    {
        public Action action;
        public Entity source;
        public Entity target;
        public Coordinate targetCoordinate;
    }

    [CreateAssetMenu(menuName = "ManaMist/States/PerformingActionState")]
    public class PerformingActionState : GameState
    {
        private Action m_Action;
        private Entity m_Source;
        private Coordinate m_TargetCoordinate;
        private Entity m_Target;
        private Dictionary<Coordinate, Path> m_Paths = new Dictionary<Coordinate, Path>();

        public override void HandleInput(InputEvent inputEvent)
        {
            if (inputEvent is MapTileClickedInput)
            {
                MapTileClickedInput mapTileClickedInput = inputEvent as MapTileClickedInput;

                m_TargetCoordinate = mapTileClickedInput.coordinate;
                MapTile mapTile = mapController.GetMapTileAtCoordinate(m_TargetCoordinate);
                if (mapTile.entities.Count > 0)
                {
                    m_Target = mapTile.entities[0];
                }
                PerformAction();
            }
        }

        protected override void Enter()
        {
            PerformingActionStateData performingActionStateData = data as PerformingActionStateData;
            m_Action = performingActionStateData.action;
            m_Source = performingActionStateData.source;
            m_TargetCoordinate = performingActionStateData.targetCoordinate;
            m_Target = performingActionStateData.target;
            ISelectableTargetAction selectableTargetAction = m_Action as ISelectableTargetAction;

            if (m_TargetCoordinate != null)
            {
                PerformAction();
            }
            else if (selectableTargetAction != null)
            {
                Coordinate currentCoordinate = mapController.GetPositionOfEntity(m_Source.id);
                m_Paths = ShowPaths(currentCoordinate, selectableTargetAction);

                foreach (Coordinate coord in m_Paths.Keys)
                {
                    mapController.GetMapTileAtCoordinate(coord).isHighlighted = true;
                }
            }
        }

        protected override void Exit()
        {
            ClearExistingHighlightedTiles();
            m_Action = null;
            m_Source = null;
            m_TargetCoordinate = null;
            m_Target = null;
            m_Paths.Clear();
        }

        private void PerformAction()
        {
            if (m_Action.CanExecute(player, m_Source, m_TargetCoordinate, m_Target))
            {
                m_Action.Execute(player, m_Source, m_TargetCoordinate, m_Target);
            }

            dispatcher.Dispatch<IdleState>();
        }

        private Dictionary<Coordinate, Path> ShowPaths(Coordinate coordinate, ISelectableTargetAction action)
        {
            if (action != null)
            {
                Pathfinding pathfinding = new Pathfinding()
                {
                    start = coordinate,
                    maxDistance = action.Range
                };

                return pathfinding.Search((end) =>
                {
                    MapTile mapTile = mapController.GetMapTileAtCoordinate(end);
                    return action.CanPerform(mapTile);
                });
            }

            return new Dictionary<Coordinate, Path>();
        }

        private void ClearExistingHighlightedTiles()
        {
            foreach (Coordinate coord in m_Paths.Keys)
            {
                mapController.GetMapTileAtCoordinate(coord).isHighlighted = false;
            }
        }
    }
}