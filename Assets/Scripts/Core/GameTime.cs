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
        
        public int Hour => (int)hours;
        public int Minute => (int)((hours - Hour) * minutesPerHour);
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
        }

        void Update()
        {
            if (settings == false)
                return;

            bool wasDay = isDay;
            AdvanceTime(Time.deltaTime);
            isDay = IsHourInDay(hours);

            if (wasDay == false && isDay)
                DayStarted?.Invoke();

            if (wasDay && isDay == false)
                NightStarted?.Invoke();
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
    }
}
