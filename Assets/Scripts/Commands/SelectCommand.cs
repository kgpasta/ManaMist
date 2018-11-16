using ManaMist.Controllers;
using ManaMist.Players;

namespace ManaMist.Commands
{
    public class SelectCommand : Command
    {
        public string id { get; set; }

        public SelectCommand(int playerId, string id) : base(playerId, CommandType.SELECT)
        {
            this.id = id;
        }

        public bool Execute(MapController mapController, Player player)
        {
            player.SelectEntity(id);
            return true;
        }

        public override string ToString()
        {
            return "Selecting " + id;
        }
    }
}