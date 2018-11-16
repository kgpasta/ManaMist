using ManaMist.Models;
using ManaMist.Utility;

namespace ManaMist.Actions
{
    public class HarvestAction : Action
    {
        public GetHarvestFunction GetHarvestAmount;

        public HarvestAction(GetHarvestFunction getHarvestFunction) : base(ActionType.HARVEST)
        {
            this.GetHarvestAmount = getHarvestFunction;
        }

        public delegate Cost GetHarvestFunction();
    }
}