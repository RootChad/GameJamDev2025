using UnityEngine;

namespace Ded
{
    public class GameStateMachine : StateMachine
    {
        public PlacementManager PlacementManager { get; private set; }
        public BlocksManager BlocksManager { get; private set; }

        [field:SerializeField] public LayerMask BlocksLayer { get; private set; }

        private void Awake()
        {
            PlacementManager = GetComponent<PlacementManager>();
            BlocksManager = GetComponent<BlocksManager>();
        }

        private void Start()
        {
            currentState = new PlacementState(this);
        }

        private void Update()
        {
            currentState?.Tick();
        }

        protected override void EnterState(State state)
        {
            if(state == null)
            {
                Debug.LogError("New state is null");
                return;
            }
            currentState?.Exit();
            currentState = state;
            currentState.Enter();
        }

        public void SwitchToSelection()
        {
            EnterState(new SelectionState(this));
        }

        public void SwitchToPlacement()
        {
            EnterState(new PlacementState(this));
        }
    }
}
