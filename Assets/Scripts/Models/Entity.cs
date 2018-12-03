using System.Collections.Generic;
using ManaMist.Actions;
using ManaMist.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace ManaMist.Models
{
    public abstract class Entity : ScriptableObject
    {
        public string id { get; set; }

        public Cost cost { get; set; }

        private List<Action> actions { get; set; } = new List<Action>();

        private void Awake()
        {
            this.id = System.Guid.NewGuid().ToString();
        }

        public void AddAction(Action action)
        {
            actions.Add(action);
        }

        public Action GetAction(ActionType type)
        {
            return actions.Find(action => action.type == type);
        }
    }
}