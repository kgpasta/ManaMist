using ManaMist.Actions;

namespace ManaMist.Models
{
    public class Mine : Building
    {
        public Cost Harvest()
        {
            return new Cost()
            {
                metal = 10
            };
        }
    }
}