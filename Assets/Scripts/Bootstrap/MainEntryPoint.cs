using Core;
using Gameplay;
using UnityEngine;

namespace Bootstrap
{
    public class MainEntryPoint : MonoBehaviour
    {
        const float npcSpacing = 1.5f;
        const float npcSpawnZ = -4f;
        const float npcSpawnY = 1f;
        const float half = 0.5f;

        [SerializeField]
        LogisticsSettings settings;

        [SerializeField]
        GameTime gameTime;

        [SerializeField]
        BoxArea boxArea;

        [SerializeField]
        Warehouse warehouse;

        [SerializeField]
        Npc npcPrefab;

        void Awake()
        {
            gameTime.Init(settings);
            boxArea.Init(settings);
            warehouse.Init(settings, boxArea);
            SpawnNpcs();
        }

        void SpawnNpcs()
        {
            if (npcPrefab == false)
            {
                Debug.LogError("MainEntryPoint needs an Npc prefab assigned.");
                return;
            }

            float startX = -(settings.NpcCount - 1) * npcSpacing * half;

            for (int index = 0; index < settings.NpcCount; index++)
            {
                Vector3 position = new Vector3(startX + index * npcSpacing, npcSpawnY, npcSpawnZ);
                Npc npc = Instantiate(npcPrefab, position, Quaternion.identity);
                npc.Init(boxArea, warehouse, settings);
            }
        }
    }
}
