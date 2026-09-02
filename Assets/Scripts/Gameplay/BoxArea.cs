using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Gameplay
{
    public class BoxArea : MonoBehaviour
    {
        [SerializeField]
        Box boxPrefab;

        LogisticsSettings settings;
        Stack<Box> idleBoxes = new Stack<Box>();
        List<Box> activeBoxes = new List<Box>();
        float spawnCooldown;

        float half = 0.5f;

        public bool TryFindAvailable(out Box box)
        {
            foreach (Box candidate in activeBoxes)
            {
                if (candidate.IsAvailable)
                {
                    box = candidate;
                    return true;
                }
            }

            box = null;
            return false;
        }

        public void Recycle(Box box)
        {
            if (box == false)
                return;

            if (activeBoxes.Remove(box) == false)
                return;

            box.ResetState();
            box.transform.SetParent(transform);
            box.gameObject.SetActive(false);
            idleBoxes.Push(box);
        }

        public void Init(LogisticsSettings logisticsSettings)
        {
            if (logisticsSettings == false)
            {
                Debug.LogError("BoxArea needs a LogisticsSettings assigned.");
                enabled = false;
                return;
            }

            if (boxPrefab == false)
            {
                Debug.LogError("BoxArea needs a Box prefab assigned.");
                enabled = false;
                return;
            }

            settings = logisticsSettings;
            WarmupPool();
        }

        void Update()
        {
            if (settings == false)
                return;

            spawnCooldown -= Time.deltaTime;

            if (spawnCooldown > 0f)
                return;

            spawnCooldown = settings.BoxSpawnInterval;
            TrySpawn();
        }

        void OnDrawGizmos()
        {
            if (settings == false)
                return;

            Gizmos.color = Color.yellow;
            Vector3 size = new Vector3(settings.SpawnAreaWidth, 0.1f, settings.SpawnAreaDepth);
            Gizmos.DrawWireCube(transform.position, size);
        }

        bool TrySpawn()
        {
            if (idleBoxes.Count == 0)
                return false;

            Box box = idleBoxes.Pop();
            box.ResetState();
            box.transform.position = GetRandomPoint();
            box.gameObject.SetActive(true);
            activeBoxes.Add(box);
            return true;
        }

        void WarmupPool()
        {
            for (int index = 0; index < settings.MaxBoxes; index++)
            {
                Box box = Instantiate(boxPrefab, transform);
                box.ResetState();
                box.gameObject.SetActive(false);
                idleBoxes.Push(box);
            }
        }

        Vector3 GetRandomPoint()
        {
            float halfWidth = settings.SpawnAreaWidth * half;
            float halfDepth = settings.SpawnAreaDepth * half;
            float x = transform.position.x + Random.Range(-halfWidth, halfWidth);
            float z = transform.position.z + Random.Range(-halfDepth, halfDepth);
            float y = transform.position.y + boxPrefab.GroundOffset;
            return new Vector3(x, y, z);
        }
    }
}
