using System.Collections.Generic;
using ManaMist.Actions;
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
        public Entity entity;
        public Coordinate currentlySelectedCoordinate;
        public Dictionary<Coordinate, Path> paths = new Dictionary<Coordinate, Path>();

        public override void HandleInput()
        {
            return;
        }

        public override void Update()
        {
            ClearExistingHighlightedTiles();

            SelectedStateData selectedStateData = data as SelectedStateData;
            currentlySelectedCoordinate = selectedStateData.coordinate;
            MapTile mapTile = mapController.GetMapTileAtCoordinate(currentlySelectedCoordinate);

            // Can move unit
            if (paths.ContainsKey(currentlySelectedCoordinate) && entity != null)
            {
                MoveAction moveAction = entity.GetAction<MoveAction>();
                if (moveAction.CanExecute(player, entity, currentlySelectedCoordinate, null))
                {
                    moveAction.Execute(player, entity, currentlySelectedCoordinate, null);
                    paths.Clear();
                }
            }
            // If we haven't selected a unit or clicked outside movement range
            else if (entity == null || !paths.ContainsKey(currentlySelectedCoordinate))
            {
                mapTile.isHighlighted = true;
                entity = mapTile.entities.Count > 0 ? mapTile.entities[0] : null;
                MoveAction moveAction = entity?.GetAction<MoveAction>();

                if (moveAction != null && moveAction.actionPoints <= entity.actionPoints)
                {
                    paths = ShowPaths(currentlySelectedCoordinate, moveAction);

                    foreach (Coordinate coord in paths.Keys)
                    {
                        mapController.GetMapTileAtCoordinate(coord).isHighlighted = true;
                    }
                }
            }
        }

        private void ClearExistingHighlightedTiles()
        {
            if (currentlySelectedCoordinate != null)
            {
                mapController.GetMapTileAtCoordinate(currentlySelectedCoordinate).isHighlighted = false;
            }

            foreach (Coordinate coord in paths.Keys)
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
                    return moveAction.CanMove(mapTile);
                });
            }

            return new Dictionary<Coordinate, Path>();
        }
    }
}