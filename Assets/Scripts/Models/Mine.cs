using ManaMist.Actions;
using UnityEngine;

namespace ManaMist.Models
{
    [CreateAssetMenu(menuName = "ManaMist/Buildings/Mine")]

    public class Mine : Building
    {
        public Cost harvestAmount;
        public override void Init()
        {
            base.Init();

            HarvestAction harvestAction = ScriptableObject.CreateInstance<HarvestAction>();
            harvestAction.harvestAmount = harvestAmount;
            AddAction(harvestAction);
        }
    }
}