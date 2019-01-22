using System.Collections;
using UnityEngine;

namespace ManaMist.Models
{
    [CreateAssetMenu(menuName = "ManaMist/Cost")]
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

        public bool CanDecrement(Cost cost)
        {
            return (food - cost.food) >= 0 && (metal - cost.metal) >= 0 && (mana - cost.mana) >= 0;
        }

        public override string ToString()
        {
            return "FOOD: " + food + ", METAL: " + metal + ", MANA: " + mana;
        }
    }
}