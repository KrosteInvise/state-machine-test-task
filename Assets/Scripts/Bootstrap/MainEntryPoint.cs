using Core;
using Gameplay;
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

        void Awake()
        {
            gameTime.Init(settings);
            boxArea.Init(settings);
            warehouse.Init(settings, boxArea);
            npcArea.Init(settings, boxArea, warehouse);
        }
    }
}
