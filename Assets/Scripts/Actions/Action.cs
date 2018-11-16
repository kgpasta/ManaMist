using System;
using ManaMist.Controllers;
using ManaMist.Models;
using ManaMist.Players;
using ManaMist.Utility;
using Newtonsoft.Json;

namespace ManaMist.Actions
{
    public class Action
    {
        public ActionType type { get; set; }

        public Action(ActionType type)
        {
            this.type = type;
        }

        public virtual bool CanExecute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
            return true;
        }

        public virtual void Execute(MapController mapController, Player player, Entity entity, Coordinate coordinate, Entity target)
        {
        }
    }

    public enum ActionType
    {
        MOVE, BUILD, HARVEST
    }

    public class ActionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(Action));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Action>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Action action = (Action)value;
            serializer.Serialize(writer, new Action(action.type), typeof(Action));
        }
    }

}