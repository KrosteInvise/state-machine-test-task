using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Core
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-100)]
    public class GameTime : MonoBehaviour
    {
        public event UnityAction DayStarted;
        public event UnityAction NightStarted;

        public const float HOURS_PER_DAY = 24f;
        private const float _MINUTES_PER_HOUR = 60f;

        [SerializeField]
        [FormerlySerializedAs("_config")]
        LogisticsSettings config;

        float hours;
        bool isDay;

        public float Hours => hours;
        public int Hour => (int)hours;
        public int Minute => (int)((hours - Hour) * _MINUTES_PER_HOUR);
        public bool IsDay => isDay;
        public bool IsNight => isDay == false;

        void Awake()
        {
            if (config == false)
            {
                Debug.LogError("GameTime needs a LogisticsSettings assigned.");
                enabled = false;
                return;
            }

            hours = config.StartHour;
            isDay = IsHourInDay(hours);
            LogCurrentTime();
        }

        void Update()
        {
            if (config == false)
            {
                return;
            }

            bool wasDay = isDay;
            int previousHour = Hour;

            AdvanceTime(Time.deltaTime);
            isDay = IsHourInDay(hours);

            if (previousHour != Hour)
            {
                LogCurrentTime();
            }

            if (wasDay == false && isDay)
            {
                DayStarted?.Invoke();
                LogPhase("Day started");
            }

            if (wasDay && isDay == false)
            {
                NightStarted?.Invoke();
                LogPhase("Night started");
            }
        }

        void AdvanceTime(float deltaTime)
        {
            float hoursPerSecond = HOURS_PER_DAY / config.SecondsPerDay;
            hours += deltaTime * hoursPerSecond;

            while (hours >= HOURS_PER_DAY)
            {
                hours -= HOURS_PER_DAY;
            }
        }

        bool IsHourInDay(float hourOfDay)
        {
            return hourOfDay >= config.DayStartHour && hourOfDay < config.NightStartHour;
        }

        void LogCurrentTime()
        {
            string phaseName = isDay ? "Day" : "Night";
            LogPhase(phaseName);
        }

        void LogPhase(string phaseName)
        {
            int hour = Hour;
            int minute = Minute;
            string clock = $"{hour:00}:{minute:00}";
            Debug.Log($"{phaseName} {clock}");
        }
    }
}
