using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManaMist.Models
{
    public enum FogOfWar
    {
        // A player's unit has never seen or visited
        // Visual: can only see the tile terrain
        INVISIBLE,
        // A player's unit has visited but currently can't see
        // Visual: can see the terrain, resource, and last seen entity on that tile(?)
        FOGGY,
        // A player's unit has visited and currently can see
        // Visual: can see all the tile's data
        VISIBLE
    }
}

