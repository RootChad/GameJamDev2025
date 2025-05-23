using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ded
{
    public class BlockFace : MonoBehaviour
    {
        [Header("Status")]
        public bool walkable;
        public bool placeable;

        [Header("Placement")]
        [SerializeField] private GameObject placementPlaceholder;

        [Header("Neighbors")]
        [field:SerializeField] public List<BlockFace> Neighbors { get; private set; } = new List<BlockFace>();

        [Header("Debug")]
        [SerializeField] private Material debugWalkableMaterial;
        [SerializeField] private Material debugUnwalkableMaterial;
        [SerializeField] private Material neighborMaterial;
        private bool debugMode = false;
        private bool neighborMode = false;

        private Renderer rdr;
        private Material materialInstance;

        private void Awake()
        {
            rdr = GetComponent<Renderer>();
            materialInstance = rdr.material;
        }

        public GameObject PlacementHolder
        {
            get
            {
                return placementPlaceholder;
            }
        }

        public void Select()
        {
            placementPlaceholder.SetActive(true);
        }

        public void Deselect()
        {
            placementPlaceholder.SetActive(false);
        }

        public void SetDebugMode(bool active)
        {
            debugMode = active;
            if (!active)
            {
                rdr.material = neighborMode ? neighborMaterial : materialInstance;
            }
            else
            {
                rdr.material = walkable ? debugWalkableMaterial : debugUnwalkableMaterial;
            }
        }

        public void ShowNeighbors(bool active)
        {
            foreach (var neighbor in Neighbors)
            {
                neighbor.ActivateNeighborMaterial(active);
            }
        }

        public void ActivateNeighborMaterial(bool active)
        {
            neighborMode = active;
            if (active)
            {
                rdr.material = neighborMaterial;
            }
            else
            {
                if (debugMode)
                {
                    rdr.material = walkable ? debugWalkableMaterial : debugUnwalkableMaterial;
                }
                else
                {
                    rdr.material = materialInstance;
                }
            }
        }
    }
}
