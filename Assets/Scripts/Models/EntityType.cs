using System;
using UnityEngine;

namespace ManaMist.Models
{
    public class EntityType : ScriptableObject
    {
        public string Name;

        public EntityClass EntityClass;

        public override bool Equals(object other)
        {
            return ((EntityType)other).Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public enum EntityClass
    {
        Melee,
        Ranged,
        Mounted,
        Flying,
        Magic,
        Creature,
        Building,
        Utility
    }
}