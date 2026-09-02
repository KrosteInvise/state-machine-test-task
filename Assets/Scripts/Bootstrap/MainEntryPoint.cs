using Core;
using Gameplay;
using UI;
using UnityEngine;

namespace Bootstrap
{
    public class MainEntryPoint : MonoBehaviour
    {
        [SerializeField]
        LogisticsSettings settings;

        [SerializeField]
        GameTime gameTime;

        [SerializeField]
        BoxArea boxArea;

        [SerializeField]
        Warehouse warehouse;

        [SerializeField]
        NpcArea npcArea;

        [SerializeField]
        DayNightLighting dayNightLighting;

        [SerializeField]
        ClockHud clockHud;

        void Awake()
        {
            gameTime.Init(settings);
            dayNightLighting.Init(gameTime);
            clockHud.Init(gameTime);
            boxArea.Init(settings);
            warehouse.Init(settings, boxArea);
            npcArea.Init(settings, boxArea, warehouse, gameTime);
        }
    }
}
