using UnityEngine;

namespace Gameplay
{
    public class NpcMoveToBoxState : IState
    {
        readonly Npc npc;

        public NpcMoveToBoxState(Npc owner)
        {
            npc = owner;
        }

        public void Enter()
        {
        }

        public void Tick(float deltaTime)
        {
            if (npc.TargetBox == false)
            {
                npc.ChangeState(npc.IdleState);
                return;
            }

            Vector3 boxPosition = npc.TargetBox.transform.position;
            npc.MoveTowards(boxPosition, deltaTime);

            if (npc.HasReached(boxPosition) == false)
                return;

            npc.ChangeState(npc.PickUpState);
        }

        public void Exit()
        {
        }
    }
}
