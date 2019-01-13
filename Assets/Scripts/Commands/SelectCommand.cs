using ManaMist.Controllers;
using ManaMist.Players;
using ManaMist.Utility;

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
            Coordinate coordinate = mapController.GetPositionOfEntity(id);
            player.SelectEntity(id, coordinate);
            return true;
        }

        public override string ToString()
        {
            return "Selecting " + id;
        }
    }
}