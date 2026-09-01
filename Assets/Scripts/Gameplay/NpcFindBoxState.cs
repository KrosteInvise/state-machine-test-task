namespace Gameplay
{
    public class NpcFindBoxState : IState
    {
        readonly Npc npc;

        public NpcFindBoxState(Npc owner)
        {
            npc = owner;
        }

        public void Enter()
        {
            if (TryReserveBox())
            {
                npc.ChangeState(npc.MoveToBoxState);
                return;
            }

            npc.ChangeState(npc.IdleState);
        }

        public void Tick(float deltaTime)
        {
        }

        public void Exit()
        {
        }

        bool TryReserveBox()
        {
            while (npc.BoxArea.TryFindAvailable(out Box box))
            {
                if (box.TryReserve(npc))
                {
                    npc.SetTargetBox(box);
                    return true;
                }
            }

            return false;
        }
    }
}
