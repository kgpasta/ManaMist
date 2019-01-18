using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/HarvestAction")]
    public class HarvestAction : Action, IBuildConstraint
    {
        public Resource resource;
        public Cost harvestAmount;

        public bool CanBuild(MapTile mapTile)
        {
            return resource == mapTile.resource;
        }
    }
}