using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Commands
{
    public class SelectCommand : Command
    {
        public Coordinate coordinate;

        public override bool Execute(MapController mapController, TurnController turnController, Player player)
        {
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
            return true;
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

        public override string ToString()
        {
            return "Selecting " + coordinate;
        }
    }
}