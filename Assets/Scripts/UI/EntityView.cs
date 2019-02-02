using ManaMist.Models;
using UnityEngine;

namespace ManaMist.UI
{
    public class EntityView : MonoBehaviour
    {
        public Entity entity;
        private Color baseColor;

        public void Initialize(Entity entity)
        {
            this.entity = entity;
            this.gameObject.name = entity.name;
            this.baseColor = entity.color;
        }

        private void OnGUI()
        {
            this.GetComponent<MeshRenderer>().material.color = entity.actionPoints > 0 ? baseColor : Color.gray;
        }
    }
}