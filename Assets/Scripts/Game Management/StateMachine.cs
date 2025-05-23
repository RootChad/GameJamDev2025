using UnityEngine;

namespace Ded
{
    [RequireComponent (typeof (PlacementManager))]
    public abstract class StateMachine : MonoBehaviour
    {
        protected State currentState;

        protected abstract void EnterState(State state);
    }
}
