using System;
using UnityEngine;

namespace Ded
{
    public class PlacementManager : MonoBehaviour
    {
        private BlocksManager blocksManager;

        [SerializeField] private Block[] blockPrefabs;
        [SerializeField] private Prop[] propPrefabs;
        [SerializeField] private FoodSource[] foodPrefabs;
        [SerializeField] private Human[] humanPrefabs;

        [SerializeField] private Transform blocksContainer;
        [SerializeField] private Transform propsContainer;
        [SerializeField] private Transform foodsContainer;
        [SerializeField] private Transform humansContainer;

        private PlaceableType currentSelectedPlaceableType;
        private PlaceableObject currentSelectedPlaceable;

        private BlockFace currentSelectedBlockFace;

        private void Awake()
        {
            blocksManager = GetComponent<BlocksManager>();
        }

        private void Start()
        {
            SelectBlock(0);
        }

        public void SelectBlock(int index)
        {
            if(index < 0 || index >= blockPrefabs.Length)
            {
                Debug.LogError("Index out of bounds");
                return;
            }
            currentSelectedPlaceableType = PlaceableType.Block;
            currentSelectedPlaceable = blockPrefabs[index];
        }

        public void SelectProp(int index)
        {
            if(index < 0 || index >= propPrefabs.Length)
            {
                Debug.LogError("Index out of bounds");
                return;
            }
            currentSelectedPlaceableType = PlaceableType.Prop;
            currentSelectedPlaceable = propPrefabs[index];
        }

        public void SelectFood(int index)
        {
            if(index < 0 || index >= foodPrefabs.Length)
            {
                Debug.LogError("Index out of bounds");
                return;
            }
            currentSelectedPlaceableType = PlaceableType.Food;
            currentSelectedPlaceable = foodPrefabs[index];
        }

        public void SelectHuman(int index = 0)
        {
            if(index < 0 || index >= humanPrefabs.Length)
            {
                Debug.LogError("Index out of bounds");
                return;
            }
            currentSelectedPlaceableType = PlaceableType.Human;
            currentSelectedPlaceable = humanPrefabs[index];
        }

        public void SetSelectedBlockFace(BlockFace blockFace)
        {
            if (currentSelectedBlockFace)
            {
                currentSelectedBlockFace.Deselect();
            }
            currentSelectedBlockFace = blockFace;
            if (currentSelectedBlockFace)
            {
                currentSelectedBlockFace.Select();
            }
        }

        public void TryPlaceBlock()
        {
            if (!currentSelectedBlockFace)
            {
                Debug.LogError("No block face selected");
                return;
            }
            Transform container = blocksContainer;
            switch (currentSelectedPlaceableType)
            {
                case PlaceableType.Block:
                    container = blocksContainer;
                    break;
                case PlaceableType.Prop:
                    container = propsContainer;
                    break;
                case PlaceableType.Food:
                    container = foodsContainer;
                    break;
                case PlaceableType.Human:
                    container = humansContainer;
                    break;
            }
            PlaceableObject placedObject = Instantiate(currentSelectedPlaceable, container);
            placedObject.transform.position = currentSelectedBlockFace.PlacementHolder.transform.position;

            if(currentSelectedPlaceableType == PlaceableType.Block)
            {
                blocksManager.AddBlock((Block)placedObject);
            }
        }
    }
}
