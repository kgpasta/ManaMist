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
        [SerializeField] protected MapController mapController;
        public int actionPoints;

        public virtual bool CanExecute(Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            return entity.actionPoints >= actionPoints;
        }

        public virtual void Execute(Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            entity.ReduceActionPoints(actionPoints);
        }
    }

}