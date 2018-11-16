using ManaMist.Controllers;

namespace ManaMist.Commands
{
    public class EndTurnCommand : Command
    {
        public EndTurnCommand(int playerId) : base(playerId, CommandType.ENDTURN)
        {

        }

        public bool Execute(TurnController turnController)
        {
            turnController.EndTurn();
            return true;
        }

        public override string ToString()
        {
            return "Ending turn of player " + playerId;
        }
    }
}