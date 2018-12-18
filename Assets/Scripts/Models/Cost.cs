using System.Collections;
using Newtonsoft.Json;
using UnityEngine;

namespace ManaMist.Models
{
    public class Cost : ScriptableObject
    {
        public int food { get; set; }

        public int metal { get; set; }

        public int mana { get; set; }

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

        public void SetCost(int food, int metal, int mana)
        {
            this.food = food;
            this.metal = metal;
            this.mana = mana;
        }
    }
}