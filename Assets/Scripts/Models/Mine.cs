using ManaMist.Actions;
using UnityEngine;

namespace ManaMist.Models
{
    [CreateAssetMenu(menuName = "ManaMist/Buildings/Mine")]

    public class Mine : Building
    {
        public Cost harvestAmount;

        private int metalHarvestAmount = 200;

        public override void Init()
        {
            base.Init();

            cost = new Cost(50, 50, 0);

            harvestAmount = new Cost(0, metalHarvestAmount, 0);

            HarvestAction harvestAction = ScriptableObject.CreateInstance<HarvestAction>();
            harvestAction.harvestAmount = harvestAmount;
            AddAction(harvestAction);
        }
    }
}