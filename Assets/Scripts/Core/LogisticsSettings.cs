using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    [CreateAssetMenu(fileName = "LogisticsSettings", menuName = "Logistics/Config")]
    public class LogisticsSettings : ScriptableObject
    {
        [SerializeField]
        [FormerlySerializedAs("_secondsPerDay")]
        [Min(1f)]
        float secondsPerDay = 120f;

        [SerializeField]
        [FormerlySerializedAs("_dayStartHour")]
        [Range(0f, GameTime.HOURS_PER_DAY)]
        float dayStartHour = 6f;

        [SerializeField]
        [FormerlySerializedAs("_nightStartHour")]
        [Range(0f, GameTime.HOURS_PER_DAY)]
        float nightStartHour = 22f;

        [SerializeField]
        [FormerlySerializedAs("_startHour")]
        [Range(0f, GameTime.HOURS_PER_DAY)]
        float startHour = 8f;

        public float SecondsPerDay => secondsPerDay;
        public float DayStartHour => dayStartHour;
        public float NightStartHour => nightStartHour;
        public float StartHour => startHour;

        void OnValidate()
        {
            secondsPerDay = Mathf.Max(1f, secondsPerDay);
            dayStartHour = WrapHour(dayStartHour);
            nightStartHour = WrapHour(nightStartHour);
            startHour = WrapHour(startHour);

            if (nightStartHour <= dayStartHour)
            {
                nightStartHour = WrapHour(dayStartHour + 1f);
            }
        }

        static float WrapHour(float hour)
        {
            float wrappedHour = hour % GameTime.HOURS_PER_DAY;

            if (wrappedHour < 0f)
            {
                wrappedHour += GameTime.HOURS_PER_DAY;
            }

            return wrappedHour;
        }
    }
}
