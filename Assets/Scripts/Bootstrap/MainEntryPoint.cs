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

        void Awake()
        {
            gameTime.Init(settings);
            boxArea.Init(settings);
        }
    }
}
