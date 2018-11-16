using ManaMist.Actions;

namespace ManaMist.Models
{
    public class Mine : Building
    {
        public Mine() : base("Mine", new Cost(), 3)
        {
            HarvestAction harvestAction = new HarvestAction(Harvest);
            this.AddAction(harvestAction);
        }

        public Cost Harvest()
        {
            return new Cost()
            {
                metal = 10
            };
        }
    }
}