using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Models
{
    [CreateAssetMenu(menuName = "ManaMist/Building")]
    public class Building : Entity
    {
        public int buildTurns { get; set; }

        public override void Init()
        {

        }
    }
}