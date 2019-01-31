using UnityEngine;

namespace ManaMist.Models
{
    public class EntityType : ScriptableObject
    {
        public string Name;

        public EntityClass EntityClass;
    }

    public enum EntityClass
    {
        Melee,
        Ranged,
        Mounted,
        Flying,
        Magic,
        Creature,
        Building
    }
}