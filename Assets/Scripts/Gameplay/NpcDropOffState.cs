namespace Gameplay
{
    public class NpcDropOffState : IState
    {
        readonly Npc npc;

        public NpcDropOffState(Npc owner)
        {
            npc = owner;
        }

        public void Enter()
        {
            if (npc.Warehouse.TryAccept(npc.CarriedBox) == false)
                return;

            npc.ClearCarriedBox();
            npc.ChangeState(npc.FindBoxState);
        }

        public void Tick(float deltaTime)
        {
            if (npc.CarriedBox == false)
            {
                npc.ChangeState(npc.FindBoxState);
                return;
            }

            if (npc.Warehouse.TryAccept(npc.CarriedBox) == false)
                return;

            npc.ClearCarriedBox();
            npc.ChangeState(npc.FindBoxState);
        }

        public void Exit()
        {
        }
    }
}
