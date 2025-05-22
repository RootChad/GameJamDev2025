using UnityEngine;

namespace Ded
{
    public class FoodSource : OrientedObject
    {
        [Header("Requirements")]
        [SerializeField] private BlockType requiredBlock;
        [SerializeField] private PropType requiredProp;

        [Header("Values")]
        [SerializeField] private int maxUses;
        [SerializeField] private float refillValue;

        private int remainingUses;

        private void Start()
        {
            remainingUses = maxUses;
        }
    }
}