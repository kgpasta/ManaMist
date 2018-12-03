using ManaMist.Actions;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Models
{
    [CreateAssetMenu(menuName = "ManaMist/Units/Worker")]
    public class Worker : Unit
    {
        public override void Init()
        {
            base.Init();

            BuildAction buildAction = ScriptableObject.CreateInstance<BuildAction>();
            buildAction.CanBuild = CanBuild;
            AddAction(buildAction);
        }

        public bool CanBuild(Coordinate currentCoordinate, Coordinate buildingCoordinate, Entity entity)
        {
            return currentCoordinate.IsAdjacent(buildingCoordinate) && entity is Building;
        }
    }
}