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
        public string Id { get { return m_Id; } }
        [SerializeField] protected EntityType m_Type;
        public EntityType Type { get { return m_Type; } }
        [SerializeField] protected Cost m_Cost;
        public Cost Cost { get { return m_Cost; } }

        [Header("Dynamic Attributes")]
        public List<Action> actions = new List<Action>();
        [SerializeField] protected int m_MaxActionPoints;
        public int ActionPoints;
        [SerializeField] protected int m_MaxHp;
        public int MaxHp { get { return m_MaxHp; } set { m_MaxHp = value; } }
        [SerializeField] protected int m_Hp;
        public int Hp
        {
            get { return m_Hp; }
            set
            {
                m_Hp = value;
                if (m_Hp <= 0)
                {
                    EntityKilled?.Invoke(this);
                }
            }
        }
        public delegate void EntityKilledDelegate(Entity entity);
        public event EntityKilledDelegate EntityKilled;
        public Color Color;

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
            ManaMistMath.Clamp(ActionPoints -= points, 0, 100);
        }

        public void ResetActionPoints()
        {
            ActionPoints = m_MaxActionPoints;
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
