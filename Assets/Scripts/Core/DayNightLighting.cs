using UnityEngine;
using UnityEngine.Rendering;

namespace Core
{
    public class DayNightLighting : MonoBehaviour
    {
        [SerializeField]
        Light sun;

        [SerializeField]
        Camera sceneCamera;

        [SerializeField]
        Color dayLightColor = new(1f, 0.96f, 0.88f);

        [SerializeField]
        Color nightLightColor = new(0.25f, 0.35f, 0.55f);

        [SerializeField]
        [Min(0f)]
        float dayIntensity = 2f;

        [SerializeField]
        [Min(0f)]
        float nightIntensity = 0.35f;

        [SerializeField]
        Color dayAmbientColor = new(0.7f, 0.75f, 0.8f);

        [SerializeField]
        Color nightAmbientColor = new(0.05f, 0.07f, 0.12f);

        [SerializeField]
        Color daySkyColor = new(0.45f, 0.65f, 0.9f);

        [SerializeField]
        Color nightSkyColor = new(0.04f, 0.06f, 0.12f);

        GameTime gameTime;

        public void Init(GameTime time)
        {
            if (time == false)
            {
                Debug.LogError("DayNightLighting needs GameTime.");
                enabled = false;
                return;
            }

            if (sun == false)
                sun = GetComponent<Light>();

            if (sun == false)
            {
                Debug.LogError("DayNightLighting needs a Light.");
                enabled = false;
                return;
            }

            gameTime = time;
            gameTime.DayStarted += ApplyView;
            gameTime.NightStarted += ApplyView;
            ApplyView();
        }

        void OnDestroy()
        {
            if (gameTime == false)
                return;

            gameTime.DayStarted -= ApplyView;
            gameTime.NightStarted -= ApplyView;
        }

        void ApplyView()
        {
            bool isNight = gameTime.IsNight;
            sun.useColorTemperature = false;
            sun.color = isNight ? nightLightColor : dayLightColor;
            sun.intensity = isNight ? nightIntensity : dayIntensity;
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = isNight ? nightAmbientColor : dayAmbientColor;

            if (sceneCamera == false)
                return;

            sceneCamera.clearFlags = CameraClearFlags.SolidColor;
            sceneCamera.backgroundColor = isNight ? nightSkyColor : daySkyColor;
        }
    }
}
