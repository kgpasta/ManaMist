using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.State
{
    public class SelectedStateData : GameStateData
    {
        public Entity entity;
        public Coordinate coordinate;
    }

    [CreateAssetMenu(menuName = "ManaMist/States/SelectedState")]
    public class SelectedState : GameState
    {
        public Coordinate currentlySelectedCoordinate;
        public Dictionary<Coordinate, Path> paths = new Dictionary<Coordinate, Path>();

        public override void HandleInput()
        {
            return;
        }

        public override void Update()
        {
            if (currentlySelectedCoordinate != null)
            {
                mapController.GetMapTileAtCoordinate(currentlySelectedCoordinate).isHighlighted = false;
            }

            SelectedStateData selectedStateData = data as SelectedStateData;
            currentlySelectedCoordinate = selectedStateData.coordinate;
            MapTile mapTile = mapController.GetMapTileAtCoordinate(currentlySelectedCoordinate);
            mapTile.isHighlighted = true;
            MoveAction moveAction = mapTile.entities.Count > 0 ? mapTile.entities[0].GetAction<MoveAction>() : null;

            if (moveAction != null)
            {
                paths = ShowPaths(currentlySelectedCoordinate, moveAction);

                foreach (Coordinate coord in paths.Keys)
                {
                    mapController.GetMapTileAtCoordinate(coord).isHighlighted = true;
                }
            }
            else
            {
                foreach (Coordinate coord in paths.Keys)
                {
                    mapController.GetMapTileAtCoordinate(coord).isHighlighted = false;
                }

                paths.Clear();
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

                return pathfinding.Search((end) => moveAction.CanMove(end));
            }

            return new Dictionary<Coordinate, Path>();
        }
    }
}