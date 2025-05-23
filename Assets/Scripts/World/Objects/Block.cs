using System.Collections.Generic;
using UnityEngine;

namespace Ded
{
    public class Block : PlaceableObject
    {
        [SerializeField] private BlockType type;
        [SerializeField] private BlockFace[] faces;
        public BlockFace[] Faces { get { return faces; } }
        [field:SerializeField] public bool IsWalkable { get; private set; }

        // Neighbors
        public Block TopLeftForwardNeighbor;
        public Block TopForwardNeighbor;
        public Block TopRightForwardNeighbor;

        public Block TopLeftNeighbor;
        public Block TopNeighbor;
        public Block TopRightNeighbor;

        public Block TopLeftBackwardNeighbor;
        public Block TopBackwardNeighbor;
        public Block TopRightBackwardNeighbor;

        public Block LeftForwardNeighbor;
        public Block ForwardNeighbor;
        public Block RightForwardNeighbor;

        public Block LeftNeighbor;
        public Block RightNeighbor;

        public Block LeftBackwardNeighbor;
        public Block BackwardNeighbor;
        public Block RightBackwardNeighbor;

        public Block BottomLeftForwardNeighbor;
        public Block BottomForwardNeighbor;
        public Block BottomRightForwardNeighbor;

        public Block BottomLeftNeighbor;
        public Block BottomNeighbor;
        public Block BottomRightNeighbor;

        public Block BottomLeftBackwardNeighbor;
        public Block BottomBackwardNeighbor;
        public Block BottomRightBackwardNeighbor;






        private List<Block> neighbors = new List<Block>();

        // Coordinates
        private float x, y, z;

        public void AddNeighbor(Block block)
        {
            if (!neighbors.Contains(block))
            {
                neighbors.Add(block);
            }
        }

        public void RemoveNeighbor(Block block)
        {
            if (neighbors.Contains(block))
            {
                neighbors.Remove(block);
            }
        }

        public void ClearNeighbors()
        {
            neighbors.Clear();
        }
    }
}