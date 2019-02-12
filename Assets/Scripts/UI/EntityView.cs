using ManaMist.Models;
using UnityEngine;

namespace ManaMist.UI
{
    public class EntityView : MonoBehaviour
    {
        public Entity entity;
        private Color m_BaseColor;

        public void Initialize(Entity entity)
        {
            this.entity = entity;
            this.gameObject.name = entity.name;
            this.m_BaseColor = entity.Color;
        }

        private void OnGUI()
        {
            this.GetComponent<MeshRenderer>().material.color = entity.ActionPoints > 0 ? m_BaseColor : Color.gray;
        }
    }
}