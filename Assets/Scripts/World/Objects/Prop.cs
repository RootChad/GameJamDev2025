using UnityEngine;

namespace Ded
{
    public class Prop : OrientedObject
    {
        [SerializeField] private PropType type;

        private Block currentBlock;
        
    }
}