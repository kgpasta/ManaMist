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
        public Dictionary<Coordinate, Path> paths;

        public override void HandleInput()
        {
            return;
        }

        public override void Update()
        {
            SelectedStateData selectedStateData = data as SelectedStateData;
            Coordinate coordinate = selectedStateData.coordinate;
            MapTile mapTile = mapController.GetMapTileAtCoordinate(coordinate);
            mapTile.isHighlighted = true;
            MoveAction moveAction = mapTile.entities.Count > 0 ? mapTile.entities[0].GetAction<MoveAction>() : null;

            if (moveAction != null)
            {
                Dictionary<Coordinate, Path> paths = ShowPaths(coordinate, moveAction);

                foreach (Coordinate coord in paths.Keys)
                {
                    mapController.GetMapTileAtCoordinate(coord).isHighlighted = true;
                }
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