using UnityEngine;
using UnityEngine.Events;

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
        private SimulationConfig _config;

        private float _hours;
        private bool _isDay;

        public float Hours => _hours;
        public int Hour => (int)_hours;
        public int Minute => (int)((_hours - Hour) * _MINUTES_PER_HOUR);
        public bool IsDay => _isDay;
        public bool IsNight => _isDay == false;

        private void Awake()
        {
            if (_config == false)
            {
                Debug.LogError("GameTime needs a SimulationConfig assigned.");
                enabled = false;
                return;
            }

            _hours = _config.StartHour;
            _isDay = IsHourInDay(_hours);
            LogCurrentTime();
        }

        private void Update()
        {
            if (_config == false)
            {
                return;
            }

            bool wasDay = _isDay;
            int previousHour = Hour;

            AdvanceTime(Time.deltaTime);
            _isDay = IsHourInDay(_hours);

            if (previousHour != Hour)
            {
                LogCurrentTime();
            }

            if (wasDay == false && _isDay)
            {
                DayStarted?.Invoke();
                LogPhase("Day started");
            }

            if (wasDay && _isDay == false)
            {
                NightStarted?.Invoke();
                LogPhase("Night started");
            }
        }

        private void AdvanceTime(float deltaTime)
        {
            float hoursPerSecond = HOURS_PER_DAY / _config.SecondsPerDay;
            _hours += deltaTime * hoursPerSecond;

            while (_hours >= HOURS_PER_DAY)
            {
                _hours -= HOURS_PER_DAY;
            }
        }

        private bool IsHourInDay(float hours)
        {
            return hours >= _config.DayStartHour && hours < _config.NightStartHour;
        }

        private void LogCurrentTime()
        {
            string phaseName = _isDay ? "Day" : "Night";
            LogPhase(phaseName);
        }

        private void LogPhase(string phaseName)
        {
            int hour = Hour;
            int minute = Minute;
            string clock = $"{hour:00}:{minute:00}";
            Debug.Log($"{phaseName} {clock}");
        }
    }
}
