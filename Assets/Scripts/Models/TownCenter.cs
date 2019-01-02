using ManaMist.Actions;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Models
{
    [CreateAssetMenu(menuName = "ManaMist/Buildings/TownCenter")]
    public class TownCenter : Building
    {
        public Cost harvestAmount;

        private const int foodHarvestAmount = 50;

        public override void Init()
        {
            base.Init();

            cost = new Cost(1000, 1000, 0);

            harvestAmount = new Cost(foodHarvestAmount, 0, 0);

            HarvestAction harvestAction = ScriptableObject.CreateInstance<HarvestAction>();
            harvestAction.harvestAmount = harvestAmount;
            AddAction(harvestAction);

            BuildAction buildAction = ScriptableObject.CreateInstance<BuildAction>();
            buildAction.CanBuild = CanBuild;
            AddAction(buildAction);
        }

        public bool CanBuild(Coordinate currentCoordinate, Coordinate coordinate, Entity entity)
        {
            return currentCoordinate.IsAdjacent(currentCoordinate) && entity is Worker;
        }
    }
}