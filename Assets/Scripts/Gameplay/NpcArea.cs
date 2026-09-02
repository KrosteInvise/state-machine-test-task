using Core;
using UnityEngine;

namespace Gameplay
{
    public class NpcArea : MonoBehaviour
    {
        [SerializeField]
        Npc npcPrefab;

        [SerializeField]
        [Min(0.1f)]
        float spacing = 1.5f;

        float half = 0.5f;
        float gizmoRadius = 0.35f;
        int gizmoSlots = 3;

        public void Init(LogisticsSettings settings, BoxArea boxArea, Warehouse warehouse, GameTime gameTime)
        {
            if (settings == false)
            {
                Debug.LogError("NpcArea needs a LogisticsSettings assigned.");
                enabled = false;
                return;
            }

            if (npcPrefab == false)
            {
                Debug.LogError("NpcArea needs an Npc prefab assigned.");
                enabled = false;
                return;
            }

            if (boxArea == false || warehouse == false || gameTime == false)
            {
                Debug.LogError("NpcArea needs BoxArea, Warehouse and GameTime assigned.");
                enabled = false;
                return;
            }

            SpawnNpcs(settings, boxArea, warehouse, gameTime);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.25f, 0.7f, 0.3f);
            Vector3 size = new Vector3(spacing * gizmoSlots, gizmoRadius, gizmoRadius);
            Gizmos.DrawWireCube(transform.position, size);
        }

        void SpawnNpcs(LogisticsSettings settings, BoxArea boxArea, Warehouse warehouse, GameTime gameTime)
        {
            float startX = -(settings.NpcCount - 1) * spacing * half;

            for (int index = 0; index < settings.NpcCount; index++)
            {
                Vector3 localOffset = new Vector3(startX + index * spacing, 0f, 0f);
                Vector3 position = transform.TransformPoint(localOffset);
                Npc npc = Instantiate(npcPrefab, position, transform.rotation, transform);
                npc.Init(boxArea, warehouse, settings, gameTime);
            }
        }
    }
}
