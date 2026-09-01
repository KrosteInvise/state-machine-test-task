using Core;
using UnityEngine;

namespace Gameplay
{
    public class Npc : MonoBehaviour
    {
        const float reachDistance = 0.6f;
        const float carryHeight = 1.2f;
        const float idleWait = 0.4f;

        [SerializeField]
        Color bodyColor = new(0.25f, 0.7f, 0.3f);

        BoxArea boxArea;
        Warehouse warehouse;
        LogisticsSettings settings;
        StateMachine stateMachine;
        NpcIdleState idleState;
        NpcFindBoxState findBoxState;
        NpcMoveToBoxState moveToBoxState;
        NpcPickUpState pickUpState;
        NpcMoveToWarehouseState moveToWarehouseState;
        NpcDropOffState dropOffState;
        Box targetBox;
        Box carriedBox;

        public BoxArea BoxArea => boxArea;
        public Warehouse Warehouse => warehouse;
        public LogisticsSettings Settings => settings;
        public NpcIdleState IdleState => idleState;
        public NpcFindBoxState FindBoxState => findBoxState;
        public NpcMoveToBoxState MoveToBoxState => moveToBoxState;
        public NpcPickUpState PickUpState => pickUpState;
        public NpcMoveToWarehouseState MoveToWarehouseState => moveToWarehouseState;
        public NpcDropOffState DropOffState => dropOffState;
        public Box TargetBox => targetBox;
        public Box CarriedBox => carriedBox;
        public float IdleWait => idleWait;

        public void Init(BoxArea area, Warehouse dropZone, LogisticsSettings logisticsSettings)
        {
            if (area == false || dropZone == false || logisticsSettings == false)
            {
                Debug.LogError("Npc needs BoxArea, Warehouse and LogisticsSettings.");
                enabled = false;
                return;
            }

            boxArea = area;
            warehouse = dropZone;
            settings = logisticsSettings;
            ApplyView();
            BuildStates();
            stateMachine.ChangeState(findBoxState);
        }

        public void ChangeState(IState nextState)
        {
            stateMachine.ChangeState(nextState);
        }

        public void SetTargetBox(Box box)
        {
            targetBox = box;
        }

        public void PickUpTarget()
        {
            if (targetBox == false)
                return;

            carriedBox = targetBox;
            targetBox = null;
            carriedBox.transform.SetParent(transform);
            carriedBox.transform.localPosition = new Vector3(0f, carryHeight, 0f);
        }

        public void ClearCarriedBox()
        {
            carriedBox = null;
        }

        public void MoveTowards(Vector3 worldPosition, float deltaTime)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = new Vector3(worldPosition.x, currentPosition.y, worldPosition.z);
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, settings.NpcMoveSpeed * deltaTime);

            Vector3 direction = targetPosition - currentPosition;
            direction.y = 0f;

            if (direction.sqrMagnitude <= 0f)
                return;

            transform.rotation = Quaternion.LookRotation(direction);
        }

        public bool HasReached(Vector3 worldPosition)
        {
            Vector3 offset = worldPosition - transform.position;
            offset.y = 0f;
            return offset.sqrMagnitude <= reachDistance * reachDistance;
        }

        void Update()
        {
            if (stateMachine == null)
                return;

            stateMachine.Tick(Time.deltaTime);
        }

        void BuildStates()
        {
            stateMachine = new StateMachine();
            idleState = new NpcIdleState(this);
            findBoxState = new NpcFindBoxState(this);
            moveToBoxState = new NpcMoveToBoxState(this);
            pickUpState = new NpcPickUpState(this);
            moveToWarehouseState = new NpcMoveToWarehouseState(this);
            dropOffState = new NpcDropOffState(this);
        }

        void ApplyView()
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            if (meshRenderer == false)
                return;

            meshRenderer.material.color = bodyColor;
        }
    }
}
