namespace Gameplay
{
    public class NpcPickUpState : IState
    {
        readonly Npc npc;

        public NpcPickUpState(Npc owner)
        {
            npc = owner;
        }

        public void Enter()
        {
            if (npc.TargetBox == false)
            {
                npc.ChangeState(npc.IdleState);
                return;
            }

            npc.PickUpTarget();
            npc.ChangeState(npc.MoveToWarehouseState);
        }

        public void Tick(float deltaTime)
        {
        }

        public void Exit()
        {
        }
    }
}
