using ManaMist.Models;
using ManaMist.Players;
using UnityEngine;

namespace ManaMist.Actions
{
    [CreateAssetMenu(menuName = "ManaMist/Actions/LimitedEntityAction")]
    public class LimitedEntityAction : Action, IBuildConstraint
    {
        [SerializeField] private EntityType m_EntityType;
        [SerializeField] private int m_EntitiesAllowed;
        public int EntitiesAllowed { get { return m_EntitiesAllowed; } set { m_EntitiesAllowed = value; } }
        public bool CanBuild(Player player, MapTile mapTile)
        {
            return player.entities.FindAll(entity => entity.type.Equals(m_EntityType)).Count < m_EntitiesAllowed;
        }
    }
}