using System;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    public class Action : ScriptableObject
    {
        public MapController mapController;
        public int actionPoints;

        public virtual bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate = null, Entity target = null)
        {
            return entity.ActionPoints >= actionPoints;
        }

        public virtual void Execute(Player player, Entity entity, Coordinate targetCoordinate = null, Entity target = null)
        {
            entity.ReduceActionPoints(actionPoints);
        }
    }

}