using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;

namespace ManaMist.Actions
{
    public class ResearchAction : Action
    {
        public ResearchBase researchBase;

        public override bool CanExecute(Player player, Entity entity, Coordinate targetCoordinate = null, Entity target = null)
        {
            return player.resources.CanDecrement(researchBase.cost)
            && !player.research.Contains(researchBase);
        }

        public override void Execute(Player player, Entity entity, Coordinate targetCoordinate = null, Entity target = null)
        {
            player.resources.Decrement(researchBase.cost);
            player.research.Add(researchBase);
        }
    }
}