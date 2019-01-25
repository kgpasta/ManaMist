using UnityEngine;

namespace ManaMist.Players
{
    public abstract class Behavior : ScriptableObject
    {
        public abstract void OnTurnStart();
    }
}

