using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ClockHud : MonoBehaviour
    {
        [SerializeField]
        TMP_Text phaseLabel;

        [SerializeField]
        TMP_Text timeLabel;

        [SerializeField]
        Color dayTextColor = Color.white;

        [SerializeField]
        Color nightTextColor = new(0.65f, 0.8f, 1f);

        GameTime gameTime;
        
        const string dayLabel = "Day";
        const string nightLabel = "Night";
        
        public void Init(GameTime time)
        {
            if (time == false)
            {
                Debug.LogError("ClockHud needs GameTime.");
                enabled = false;
                return;
            }

            if (phaseLabel == false || timeLabel == false)
            {
                Debug.LogError("ClockHud needs phase and time labels assigned.");
                enabled = false;
                return;
            }

            gameTime = time;
            Draw();
        }

        void Update()
        {
            Draw();
        }

        void Draw()
        {
            int hour = gameTime.Hour;
            int minute = gameTime.Minute;
            string clock = $"{hour:00}:{minute:00}";
            Color color = gameTime.IsNight ? nightTextColor : dayTextColor;

            phaseLabel.text = gameTime.IsNight ? nightLabel : dayLabel;
            phaseLabel.color = color;
            timeLabel.text = clock;
            timeLabel.color = color;
        }
    }
}
