using ManaMist.Models;

namespace ManaMist.Actions
{
    public interface ISelectableTargetAction
    {
        int Range { get; }

        bool CanPerform(MapTile mapTile);
    }
}