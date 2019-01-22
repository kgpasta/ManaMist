using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Input;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.State
{
    public class SelectedStateData : GameStateData
    {
        public Coordinate coordinate;
    }

    [CreateAssetMenu(menuName = "ManaMist/States/SelectedState")]
    public class SelectedState : GameState
    {
        [SerializeField] private Entity m_CurrentlySelectedEntity;
        public Entity CurrentlySelectedEntity { get { return m_CurrentlySelectedEntity; } }

        [SerializeField] private Coordinate m_CurrentlySelectedCoordinate;
        public Coordinate CurrentlySelectedCoordinate { get { return m_CurrentlySelectedCoordinate; } }

        private Dictionary<Coordinate, Path> m_Paths = new Dictionary<Coordinate, Path>();

        protected override void Enter()
        {
            SelectedStateData selectedStateData = data as SelectedStateData;
            m_CurrentlySelectedCoordinate = selectedStateData.coordinate;
            MapTile mapTile = mapController.GetMapTileAtCoordinate(m_CurrentlySelectedCoordinate);
            m_CurrentlySelectedEntity = mapTile.entities.Count > 0 ? mapTile.entities[0] : null;

            if (m_CurrentlySelectedEntity != null)
            {
                mapTile.isHighlighted = true;
                MoveAction moveAction = m_CurrentlySelectedEntity.GetAction<MoveAction>();

                if (moveAction != null && moveAction.actionPoints <= m_CurrentlySelectedEntity.actionPoints)
                {
                    m_Paths = ShowPaths(m_CurrentlySelectedCoordinate, moveAction);

                    foreach (Coordinate coord in m_Paths.Keys)
                    {
                        mapController.GetMapTileAtCoordinate(coord).isHighlighted = true;
                    }
                }
            }
        }

        protected override void Exit()
        {
            ClearExistingHighlightedTiles();
            m_CurrentlySelectedEntity = null;
            m_CurrentlySelectedCoordinate = null;
            m_Paths.Clear();
        }

        public override void HandleInput(InputEvent inputEvent)
        {
            if (inputEvent is MapTileClickedInput)
            {
                MapTileClickedInput mapTileClickedInput = inputEvent as MapTileClickedInput;
                MoveAction moveAction = m_CurrentlySelectedEntity.GetAction<MoveAction>();

                if (m_Paths.ContainsKey(mapTileClickedInput.coordinate) && moveAction.CanExecute(player, m_CurrentlySelectedEntity, mapTileClickedInput.coordinate, null))
                {
                    PerformingActionStateData stateData = ScriptableObject.CreateInstance<PerformingActionStateData>();
                    stateData.action = moveAction;
                    stateData.source = m_CurrentlySelectedEntity;
                    stateData.targetCoordinate = mapTileClickedInput.coordinate;

                    dispatcher.Dispatch<PerformingActionState>(stateData);
                }
                else
                {
                    dispatcher.Dispatch<IdleState>();
                }
            }

            if (inputEvent is ActionButtonClickedInput)
            {
                ActionButtonClickedInput actionButtonClickedInput = inputEvent as ActionButtonClickedInput;
                PerformingActionStateData stateData = ScriptableObject.CreateInstance<PerformingActionStateData>();
                stateData.action = m_CurrentlySelectedEntity.GetAction(actionButtonClickedInput.actionType);
                stateData.source = m_CurrentlySelectedEntity;
                stateData.target = actionButtonClickedInput.target;

                dispatcher.Dispatch<PerformingActionState>(stateData);
            }
        }

        private void ClearExistingHighlightedTiles()
        {
            if (m_CurrentlySelectedCoordinate != null)
            {
                mapController.GetMapTileAtCoordinate(m_CurrentlySelectedCoordinate).isHighlighted = false;
            }

            foreach (Coordinate coord in m_Paths.Keys)
            {
                mapController.GetMapTileAtCoordinate(coord).isHighlighted = false;
            }
        }

        private Dictionary<Coordinate, Path> ShowPaths(Coordinate coordinate, MoveAction moveAction)
        {
            if (moveAction != null)
            {
                Pathfinding pathfinding = new Pathfinding()
                {
                    start = coordinate,
                    maxDistance = moveAction.movementRange
                };

                return pathfinding.Search((end) =>
                {
                    MapTile mapTile = mapController.GetMapTileAtCoordinate(end);
                    return moveAction.CanPerform(mapTile);
                });
            }

            return new Dictionary<Coordinate, Path>();
        }
    }
}