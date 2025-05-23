using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ded
{
    public class PlacementState : GameState
    {
        public PlacementState(GameStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
        }

        public override void Tick()
        {
            CheckBlock();
            HandleInput();
        }

        public override void Exit()
        {
        }

        private void CheckBlock()
        {
            RaycastHit hit;
            BlockFace blockFace = null;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, stateMachine.BlocksLayer))
            {
                blockFace = hit.collider.GetComponent<BlockFace>();
            }
            stateMachine.PlacementManager.SetSelectedBlockFace(blockFace);
        }

        private void HandleInput()
        {
            if(Mouse.current.leftButton.wasReleasedThisFrame)
            {
                stateMachine.PlacementManager.TryPlaceBlock();
            }
        }
    }
}
