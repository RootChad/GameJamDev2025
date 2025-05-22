using UnityEngine;

namespace Ded
{
    public class BlockFace : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] private bool walkable;
        [SerializeField] private bool placable;

        [Header("Placement")]
        [SerializeField] private GameObject placementPlaceholder;

    }
}
