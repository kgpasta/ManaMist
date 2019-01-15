using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;

namespace ManaMist.Commands
{
    public abstract class Command
    {
        public abstract bool Execute(MapController mapController, TurnController turnController, Player player);
    }
}