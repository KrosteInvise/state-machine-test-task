using UnityEngine;

namespace Gameplay
{
    public class Box : MonoBehaviour
    {
        [SerializeField]
        Color availableColor = new(0.76f, 0.45f, 0.18f);

        [SerializeField]
        Color reservedColor = new(0.35f, 0.35f, 0.38f);

        MeshRenderer meshRenderer;
        object owner;

        public float GroundOffset => transform.localScale.y * 0.5f;
        public bool IsAvailable => owner == null;
        public bool IsReserved => owner != null;

        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            ApplyView();
        }

        public bool TryReserve(object taker)
        {
            if (taker == null)
                return false;

            if (IsAvailable == false)
                return false;

            owner = taker;
            ApplyView();
            return true;
        }

        public void Release(object taker)
        {
            if (owner != taker)
                return;

            owner = null;
            ApplyView();
        }

        public void ResetState()
        {
            owner = null;
            ApplyView();
        }

        void ApplyView()
        {
            if (meshRenderer == false)
                return;

            Color color = IsReserved ? reservedColor : availableColor;
            meshRenderer.material.color = color;
        }
    }
}
