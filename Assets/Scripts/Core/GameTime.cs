using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class GameTime : MonoBehaviour
    {
        public event UnityAction DayStarted;
        public event UnityAction NightStarted;

        public const float HoursPerDay = 24f;
        const float minutesPerHour = 60f;

        LogisticsSettings settings;
        float hours;
        bool isDay;

        public float Hours => hours;
        public int Hour => (int)hours;
        public int Minute => (int)((hours - Hour) * minutesPerHour);
        public bool IsDay => isDay;
        public bool IsNight => isDay == false;

        public void Init(LogisticsSettings logisticsSettings)
        {
            if (logisticsSettings == false)
            {
                Debug.LogError("GameTime needs a LogisticsSettings assigned.");
                enabled = false;
                return;
            }

            settings = logisticsSettings;
            hours = settings.StartHour;
            isDay = IsHourInDay(hours);
            LogCurrentTime();
        }

        void Update()
        {
            if (settings == false)
                return;

            bool wasDay = isDay;
            int previousHour = Hour;

            AdvanceTime(Time.deltaTime);
            isDay = IsHourInDay(hours);

            if (previousHour != Hour)
                LogCurrentTime();

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
            float hoursPerSecond = HoursPerDay / settings.SecondsPerDay;
            hours += deltaTime * hoursPerSecond;

            while (hours >= HoursPerDay)
            {
                hours -= HoursPerDay;
            }
        }

        bool IsHourInDay(float hourOfDay)
        {
            return hourOfDay >= settings.DayStartHour && hourOfDay < settings.NightStartHour;
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
