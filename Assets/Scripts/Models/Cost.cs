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

        public Cost(int food, int metal, int mana)
        {
            this.food = food;
            this.metal = metal;
            this.mana = mana;
        }

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