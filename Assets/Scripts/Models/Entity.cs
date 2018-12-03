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
        public string id;

        public Cost cost;

        public List<Action> actions;

        public virtual void Awake()
        {
            this.id = System.Guid.NewGuid().ToString();
            this.actions = new List<Action>();
            Init();
        }

        public abstract void Init();

        public void AddAction(Action action)
        {
            actions.Add(action);
        }

        public T GetAction<T>() where T : Action
        {
            return actions.Find(action => action is T) as T;
        }
    }
}