using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ManaMist.Actions;
using ManaMist.Players;
using ManaMist.Utility;
using UnityEngine;

namespace ManaMist.Models
{
    public abstract class Entity : ScriptableObject
    {
        [Header("Static Attributes")]
        [SerializeField] protected string m_Id;
        public string id { get { return m_Id; } }
        [SerializeField] protected EntityType m_Type;
        public EntityType type { get { return m_Type; } }
        [SerializeField] protected Cost m_Cost;
        public Cost cost { get { return m_Cost; } }

        [Header("Dynamic Attributes")]
        public List<Action> actions = new List<Action>();
        [SerializeField] protected int maxActionPoints;
        public int actionPoints;
        [SerializeField] protected int maxHp;
        public int MaxHp { get { return maxHp; } }
        public int hp;
        public int maxHp;

        private void Awake()
        {
            m_Id = System.Guid.NewGuid().ToString();
        }

        public void AddAction(Action action)
        {
            actions.Add(action);
        }

        public Action GetAction(System.Type type)
        {
            return actions.Find(action => action.GetType() == type);
        }

        public T GetAction<T>() where T : Action
        {
            return actions.Find(action => action is T) as T;
        }

        public List<T> GetActions<T>()
        {
            return actions.FindAll(action => action is T).Cast<T>().ToList();
        }

        public void ReduceActionPoints(int points)
        {
            ManaMistMath.Clamp(actionPoints -= points, 0, 100);
        }

        public void ResetActionPoints()
        {
            actionPoints = maxActionPoints;
        }

        public void PerformTurnStartActions(Player player)
        {
            foreach (Action action in actions)
            {
                if (action is ITurnStartAction)
                {
                    if (action.CanExecute(player, this))
                    {
                        action.Execute(player, this);
                    }

                    ITurnStartAction turnStartAction = action as ITurnStartAction;
                    turnStartAction.TurnsLeft--;
                    if (turnStartAction.TurnsLeft == 0)
                    {
                        actions.Remove(action);
                    }
                }
            }
        }
    }
}
