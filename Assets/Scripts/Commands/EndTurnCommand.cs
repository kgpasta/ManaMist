using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;

namespace ManaMist.Commands
{
    public class EndTurnCommand : Command
    {
        public override bool Execute(MapController mapController, TurnController turnController, Player player)
        {
            turnController.EndTurn();
            return true;
        }

        public override string ToString()
        {
            return "Ending turn of player ";
        }
    }
}