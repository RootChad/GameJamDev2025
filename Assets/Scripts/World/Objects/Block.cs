using System.Collections.Generic;
using UnityEngine;

namespace Ded
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private BlockType type;
        [SerializeField] private BlockFace[] faces;
        
        private List<Block> neighbors = new List<Block>();
        // Coordinates
        private float x, y, z;
    }
}