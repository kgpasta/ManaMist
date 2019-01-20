using ManaMist.Models;
using UnityEngine;

namespace ManaMist.Combat
{
    [CreateAssetMenu(menuName = "ManaMist/Combat Entity")]
    public class CombatEntity : Entity
    {
        public int hp;
        public int attack;
        public int defense;
        public int skill;
        public int accuracy;
        public int speed;
        public int range;
    }
}