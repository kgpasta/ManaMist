using ManaMist.Models;
using ManaMist.Utility;

namespace ManaMist.Input
{
    public class MapTileClickedInput : InputEvent
    {
        public Coordinate coordinate;
        public MapTile mapTile;
    }
}