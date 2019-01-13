using System;
using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.State
{
    public class SelectedState : IPlayerState
    {
        public Entity entity;
        public Coordinate coordinate;
        public Dictionary<Coordinate, Path> paths;

        public void Update()
        {
            MoveAction moveAction = entity.GetAction<MoveAction>();

            if (moveAction != null)
            {
                Pathfinding pathfinding = new Pathfinding()
                {
                    start = coordinate,
                    maxDistance = moveAction.movementRange
                };

                paths = pathfinding.Search((end) => moveAction.CanMove(end));
            }
        }
    }
}