using System.Collections;
using Newtonsoft.Json;

namespace ManaMist.Models
{
    public class Cost
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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(
                this, Formatting.Indented,
                new JsonConverter[] { });
        }
    }
}