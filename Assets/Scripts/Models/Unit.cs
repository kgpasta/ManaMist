using ManaMist.Actions;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Models
{
    [CreateAssetMenu(menuName = "ManaMist/Unit")]
    public class Unit : Entity
    {
        public bool CanMove(MapTile mapTile)
        {
            return mapTile.terrain != Terrain.WATER && mapTile.entities.Count == 0;
        }
    }
}