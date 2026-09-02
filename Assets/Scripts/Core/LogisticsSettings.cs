using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "LogisticsSettings", menuName = "Logistics/Config")]
    public class LogisticsSettings : ScriptableObject
    {
        [Header("Time")]
        [SerializeField]
        [Min(1f)]
        float secondsPerDay = 120f;

        [SerializeField]
        [Range(0f, GameTime.HoursPerDay)]
        float dayStartHour = 6f;

        [SerializeField]
        [Range(0f, GameTime.HoursPerDay)]
        float nightStartHour = 22f;

        [SerializeField]
        [Range(0f, GameTime.HoursPerDay)]
        float startHour = 8f;

        [Header("Boxes")]
        [SerializeField]
        [Min(0.1f)]
        float boxSpawnInterval = 3f;

        [SerializeField]
        [Min(1f)]
        float spawnAreaWidth = 8f;

        [SerializeField]
        [Min(1f)]
        float spawnAreaDepth = 8f;

        [SerializeField]
        [Min(1)]
        int maxBoxes = 12;

        [Header("Warehouse")]
        [SerializeField]
        [Min(1f)]
        float warehouseWidth = 3f;

        [SerializeField]
        [Min(1f)]
        float warehouseDepth = 3f;

        [Header("NPCs")]
        [SerializeField]
        [Min(1)]
        int npcCount = 4;

        [SerializeField]
        [Min(0.1f)]
        float npcMoveSpeed = 3.5f;

        [SerializeField]
        [Min(0.1f)]
        float npcNightMoveSpeed = 1.1f;

        public float SecondsPerDay => secondsPerDay;
        public float DayStartHour => dayStartHour;
        public float NightStartHour => nightStartHour;
        public float StartHour => startHour;
        public float BoxSpawnInterval => boxSpawnInterval;
        public float SpawnAreaWidth => spawnAreaWidth;
        public float SpawnAreaDepth => spawnAreaDepth;
        public int MaxBoxes => maxBoxes;
        public float WarehouseWidth => warehouseWidth;
        public float WarehouseDepth => warehouseDepth;
        public int NpcCount => npcCount;
        public float NpcMoveSpeed => npcMoveSpeed;
        public float NpcNightMoveSpeed => npcNightMoveSpeed;

        void OnValidate()
        {
            secondsPerDay = Mathf.Max(1f, secondsPerDay);
            dayStartHour = WrapHour(dayStartHour);
            nightStartHour = WrapHour(nightStartHour);
            startHour = WrapHour(startHour);

            if (nightStartHour <= dayStartHour)
                nightStartHour = WrapHour(dayStartHour + 1f);

            boxSpawnInterval = Mathf.Max(0.1f, boxSpawnInterval);
            spawnAreaWidth = Mathf.Max(1f, spawnAreaWidth);
            spawnAreaDepth = Mathf.Max(1f, spawnAreaDepth);
            maxBoxes = Mathf.Max(1, maxBoxes);
            warehouseWidth = Mathf.Max(1f, warehouseWidth);
            warehouseDepth = Mathf.Max(1f, warehouseDepth);
            npcCount = Mathf.Max(1, npcCount);
            npcMoveSpeed = Mathf.Max(0.1f, npcMoveSpeed);
            npcNightMoveSpeed = Mathf.Max(0.1f, npcNightMoveSpeed);
        }

        float WrapHour(float hour)
        {
            float wrappedHour = hour % GameTime.HoursPerDay;

            if (wrappedHour < 0f)
                wrappedHour += GameTime.HoursPerDay;

            return wrappedHour;
        }
    }
}
