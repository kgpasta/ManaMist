using ManaMist.Models;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/HarvestAction")]
    public class HarvestAction : Action
    {
        public Cost harvestAmount;
    }
}