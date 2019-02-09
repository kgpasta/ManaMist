using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;

namespace ManaMist.Actions
{
    public class ResearchAction : Action
    {
        public ResearchBase research;

        public override bool CanExecute(Player player, Entity entity = null, Coordinate targetCoordinate = null, Entity target = null)
        {
            return player.resources.CanDecrement(research.cost)
            && !player.research.Contains(research);
        }

        public override void Execute(Player player, Entity entity = null, Coordinate targetCoordinate = null, Entity target = null)
        {
            player.resources.Decrement(research.cost);
            player.research.Add(research);
            research.PerformResearch(player);
        }
    }
}