using UnityEngine;

namespace Ded
{
    public abstract class GameState : State
    {
        protected GameStateMachine stateMachine;

        public GameState(GameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
