using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ManaMist.Models
{
    public abstract class Entity
    {
        public string id { get; set; }

        public string name { get; set; }

        public Cost cost { get; set; }

        private Dictionary<ActionType, Action> actions { get; set; }

        public Entity(string name, Cost cost)
        {
            this.id = System.Guid.NewGuid().ToString();
            this.name = name;
            this.cost = cost;
            this.actions = new Dictionary<ActionType, Action>();
        }

        public void AddAction(Action action)
        {
            actions[action.type] = action;
        }

        public Action GetAction(ActionType type)
        {
            if (actions.ContainsKey(type))
            {
                return actions[type];
            }
            return null;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(
           this, Formatting.Indented,
           new JsonConverter[] { new StringEnumConverter(), new ActionConverter() });
        }
    }
}