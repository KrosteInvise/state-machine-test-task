using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "SimulationConfig", menuName = "Simulation/Config")]
    public class SimulationConfig : ScriptableObject
    {
        [SerializeField]
        [Min(1f)]
        private float _secondsPerDay = 120f;

        [SerializeField]
        [Range(0f, GameTime.HOURS_PER_DAY)]
        private float _dayStartHour = 6f;

        [SerializeField]
        [Range(0f, GameTime.HOURS_PER_DAY)]
        private float _nightStartHour = 22f;

        [SerializeField]
        [Range(0f, GameTime.HOURS_PER_DAY)]
        private float _startHour = 8f;

        public float SecondsPerDay => _secondsPerDay;
        public float DayStartHour => _dayStartHour;
        public float NightStartHour => _nightStartHour;
        public float StartHour => _startHour;

        private void OnValidate()
        {
            _secondsPerDay = Mathf.Max(1f, _secondsPerDay);
            _dayStartHour = WrapHour(_dayStartHour);
            _nightStartHour = WrapHour(_nightStartHour);
            _startHour = WrapHour(_startHour);

            if (_nightStartHour <= _dayStartHour)
            {
                _nightStartHour = WrapHour(_dayStartHour + 1f);
            }
        }

        private static float WrapHour(float hour)
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
