namespace Gameplay
{
    public class NpcIdleState : IState
    {
        readonly Npc npc;
        float waitTime;

        public NpcIdleState(Npc owner)
        {
            npc = owner;
        }

        public void Enter()
        {
            waitTime = 0f;
        }

        public void Tick(float deltaTime)
        {
            waitTime += deltaTime;

            if (waitTime < npc.IdleWait)
                return;

            npc.ChangeState(npc.FindBoxState);
        }

        public void Exit()
        {
        }
    }
}
