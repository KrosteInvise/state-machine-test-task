using Core;
using UnityEngine;

namespace Gameplay
{
    public class Warehouse : MonoBehaviour
    {
        const float half = 0.5f;
        const float platformHeight = 0.3f;

        [SerializeField]
        Color platformColor = new(0.2f, 0.45f, 0.75f);

        LogisticsSettings settings;
        BoxArea boxArea;
        MeshRenderer meshRenderer;

        public Vector3 DropPoint => transform.position;

        public void Init(LogisticsSettings logisticsSettings, BoxArea area)
        {
            if (logisticsSettings == false)
            {
                Debug.LogError("Warehouse needs a LogisticsSettings assigned.");
                enabled = false;
                return;
            }

            if (area == false)
            {
                Debug.LogError("Warehouse needs a BoxArea assigned.");
                enabled = false;
                return;
            }

            settings = logisticsSettings;
            boxArea = area;
            meshRenderer = GetComponent<MeshRenderer>();
            ApplySize();
            ApplyView();
        }

        public bool TryAccept(Box box)
        {
            if (box == false)
                return false;

            if (boxArea == false)
                return false;

            boxArea.Recycle(box);
            return true;
        }

        public bool Contains(Vector3 position)
        {
            if (settings == false)
                return false;

            Vector3 offset = position - transform.position;
            float halfWidth = settings.WarehouseWidth * half;
            float halfDepth = settings.WarehouseDepth * half;
            return Mathf.Abs(offset.x) <= halfWidth && Mathf.Abs(offset.z) <= halfDepth;
        }

        void OnDrawGizmos()
        {
            if (settings == false)
                return;

            Gizmos.color = platformColor;
            Vector3 size = new Vector3(settings.WarehouseWidth, platformHeight, settings.WarehouseDepth);
            Gizmos.DrawWireCube(transform.position, size);
        }

        void ApplySize()
        {
            Vector3 position = transform.position;
            transform.position = new Vector3(position.x, platformHeight * half, position.z);
            transform.localScale = new Vector3(settings.WarehouseWidth, platformHeight, settings.WarehouseDepth);
        }

        void ApplyView()
        {
            if (meshRenderer == false)
                return;

            meshRenderer.material.color = platformColor;
        }
    }
}
