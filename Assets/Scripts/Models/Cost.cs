using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

namespace ManaMist.Models
{
    public class Cost : ScriptableObject
    {
        public int food;
        public int metal;
        public int mana;

        public void Increment(Cost cost)
        {
            food += cost.food;
            metal += cost.metal;
            mana += cost.mana;
        }

        public void Decrement(Cost cost)
        {
            food -= cost.food;
            metal -= cost.metal;
            mana -= cost.mana;
        }

        public override string ToString()
        {
            return "FOOD: " + food + ", METAL: " + metal + ", MANA: " + mana;
        }
    }
}