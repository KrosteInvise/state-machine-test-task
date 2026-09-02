using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    [CreateAssetMenu(fileName = "LogisticsSettings", menuName = "Logistics/Config")]
    public class LogisticsSettings : ScriptableObject
    {
        [Header("Time")]
        [SerializeField]
        [FormerlySerializedAs("_secondsPerDay")]
        [Min(1f)]
        float secondsPerDay = 120f;

        [SerializeField]
        [FormerlySerializedAs("_dayStartHour")]
        [Range(0f, GameTime.HoursPerDay)]
        float dayStartHour = 6f;

        [SerializeField]
        [FormerlySerializedAs("_nightStartHour")]
        [Range(0f, GameTime.HoursPerDay)]
        float nightStartHour = 22f;

        [SerializeField]
        [FormerlySerializedAs("_startHour")]
        [Range(0f, GameTime.HoursPerDay)]
        float startHour = 8f;

        [Header("Boxes")]
        [SerializeField]
        [Min(0.1f)]
        [Tooltip("Real seconds between box appearances.")]
        float boxSpawnInterval = 3f;

        [SerializeField]
        [Min(1f)]
        [Tooltip("Width of the random spawn area along X.")]
        float spawnAreaWidth = 8f;

        [SerializeField]
        [Min(1f)]
        [Tooltip("Depth of the random spawn area along Z.")]
        float spawnAreaDepth = 8f;

        [SerializeField]
        [Min(1)]
        [Tooltip("How many boxes may exist at once.")]
        int maxBoxes = 12;

        [Header("Warehouse")]
        [SerializeField]
        [Min(1f)]
        [Tooltip("Width of the warehouse drop-off zone along X.")]
        float warehouseWidth = 3f;

        [SerializeField]
        [Min(1f)]
        [Tooltip("Depth of the warehouse drop-off zone along Z.")]
        float warehouseDepth = 3f;

        [Header("NPCs")]
        [SerializeField]
        [Min(1)]
        [Tooltip("How many NPCs work at the same time.")]
        int npcCount = 4;

        [SerializeField]
        [Min(0.1f)]
        [Tooltip("NPC movement speed in meters per second.")]
        float npcMoveSpeed = 3.5f;

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
        }

        static float WrapHour(float hour)
        {
            float wrappedHour = hour % GameTime.HoursPerDay;

            if (wrappedHour < 0f)
                wrappedHour += GameTime.HoursPerDay;

            return wrappedHour;
        }
    }
}
