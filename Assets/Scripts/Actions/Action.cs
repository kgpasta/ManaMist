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

        public virtual bool CanExecute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            return true;
        }

        public virtual void Execute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
        }
    }

}