using ManaMist.Actions;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Models
{
    [CreateAssetMenu(menuName = "ManaMist/Buildings/TownCenter")]
    public class TownCenter : Building
    {
        public override void Init()
        {
            base.Init();

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