namespace Gameplay
{
    public class NpcMoveToWarehouseState : IState
    {
        readonly Npc npc;

        public NpcMoveToWarehouseState(Npc owner)
        {
            npc = owner;
        }

        public void Enter()
        {
        }

        public void Tick(float deltaTime)
        {
            if (npc.CarriedBox == false)
            {
                npc.ChangeState(npc.IdleState);
                return;
            }

            npc.MoveTowards(npc.Warehouse.DropPoint, deltaTime);

            if (npc.Warehouse.Contains(npc.transform.position) == false)
                return;

            npc.ChangeState(npc.DropOffState);
        }

        public void Exit()
        {
        }
    }
}
