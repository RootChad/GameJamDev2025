using System;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

namespace Ded
{
    public class BlocksManager : MonoBehaviour
    {
        [SerializeField] private Block coreBlock;

        private Dictionary<string, Block> blocks = new Dictionary<string, Block>();

        private BlockFace currentHoveredBlockFace;
        private BlockFace currentSelectedBlockFace;

        private void Start()
        {
            blocks.Add("000", coreBlock);
        }

        public void AddBlock(Block block)
        {
            Vector3 placedObjectPosition = block.transform.position;
            string indexString = $"{placedObjectPosition.x}{placedObjectPosition.y}{placedObjectPosition.z}";
            blocks.Add(indexString, block);

            UpdateBlockNeighbors(block);
            UpdateBlockFacesNeighborhood(block);
            if (block.IsWalkable)
            {
                UpdateWalkableFacesByPosition(block);
                UpdateWalkableFacesByNeighborhood(block);
            }
            else
            {
                foreach(var face in block.Faces)
                {
                    face.walkable = false;
                }
            }
        }

        private void UpdateBlockNeighbors(Block block)
        {
            float blockX = block.transform.position.x;
            float blockY = block.transform.position.y;
            float blockZ = block.transform.position.z;

            // Top Left Forward
            string topLeftForwardNeighborIndex = $"{blockX - 1}{blockY + 1}{blockZ + 1}";
            if (blocks.ContainsKey(topLeftForwardNeighborIndex))
            {
                Block neighbor = blocks[topLeftForwardNeighborIndex];
                block.TopLeftForwardNeighbor = neighbor;
                neighbor.BottomRightBackwardNeighbor = block;
            }
            else
            {
                block.TopLeftForwardNeighbor = null;
            }

            // Top Forward
            string topForwardNeighborIndex = $"{blockX}{blockY + 1}{blockZ + 1}";
            if (blocks.ContainsKey(topForwardNeighborIndex))
            {
                Block neighbor = blocks[topForwardNeighborIndex];
                block.TopForwardNeighbor = neighbor;
                neighbor.BottomBackwardNeighbor = block;
            }
            else
            {
                block.TopForwardNeighbor = null;
            }

            // Top Right Forward
            string topRightForwardNeighborIndex = $"{blockX + 1}{blockY + 1}{blockZ + 1}";
            if (blocks.ContainsKey(topRightForwardNeighborIndex))
            {
                Block neighbor = blocks[topRightForwardNeighborIndex];
                block.TopRightForwardNeighbor = neighbor;
                neighbor.BottomLeftBackwardNeighbor = block;
            }
            else
            {
                block.TopRightForwardNeighbor = null;
            }


            // Top Left
            string topLeftNeighborIndex = $"{blockX - 1}{blockY + 1}{blockZ}";
            if (blocks.ContainsKey(topLeftNeighborIndex))
            {
                Block neighbor = blocks[topLeftNeighborIndex];
                block.TopLeftNeighbor = neighbor;
                neighbor.BottomRightNeighbor = block;
            }
            else
            {
                block.TopLeftNeighbor = null;
            }

            // Top
            string topNeighborIndex = $"{blockX}{blockY + 1}{blockZ}";
            if (blocks.ContainsKey(topNeighborIndex))
            {
                Block neighbor = blocks[topNeighborIndex];
                block.TopNeighbor = neighbor;
                neighbor.BottomNeighbor = block;
            }
            else
            {
                block.TopNeighbor = null;
            }

            // Top Right
            string topRightNeighborIndex = $"{blockX + 1}{blockY + 1}{blockZ}";
            if (blocks.ContainsKey(topRightNeighborIndex))
            {
                Block neighbor = blocks[topRightNeighborIndex];
                block.TopRightNeighbor = neighbor;
                neighbor.BottomLeftNeighbor = block;
            }
            else
            {
                block.TopRightNeighbor = null;
            }


            // Top Left Backward
            string topLeftBackwardNeighborIndex = $"{blockX - 1}{blockY + 1}{blockZ - 1}";
            if (blocks.ContainsKey(topLeftBackwardNeighborIndex))
            {
                Block neighbor = blocks[topLeftBackwardNeighborIndex];
                block.TopLeftBackwardNeighbor = neighbor;
                neighbor.BottomRightForwardNeighbor = block;
            }
            else
            {
                block.TopLeftBackwardNeighbor = null;
            }

            // Top Backward
            string topBackwardNeighborIndex = $"{blockX}{blockY + 1}{blockZ - 1}";
            if (blocks.ContainsKey(topBackwardNeighborIndex))
            {
                Block neighbor = blocks[topBackwardNeighborIndex];
                block.TopBackwardNeighbor = neighbor;
                neighbor.BottomForwardNeighbor = block;
            }
            else
            {
                block.TopBackwardNeighbor = null;
            }

            // Top Right Backward
            string topRightBackwardNeighborIndex = $"{blockX + 1}{blockY + 1}{blockZ - 1}";
            if (blocks.ContainsKey(topRightBackwardNeighborIndex))
            {
                Block neighbor = blocks[topRightBackwardNeighborIndex];
                block.TopRightBackwardNeighbor = neighbor;
                neighbor.BottomLeftForwardNeighbor = block;
            }
            else
            {
                block.TopRightBackwardNeighbor = null;
            }


            // Left Forward
            string leftForwardNeighborIndex = $"{blockX - 1}{blockY}{blockZ + 1}";
            if (blocks.ContainsKey(leftForwardNeighborIndex))
            {
                Block neighbor = blocks[leftForwardNeighborIndex];
                block.LeftForwardNeighbor = neighbor;
                neighbor.RightBackwardNeighbor = block;
            }
            else
            {
                block.LeftForwardNeighbor = null;
            }

            // Forward
            string forwardNeighborIndex = $"{blockX}{blockY}{blockZ + 1}";
            if (blocks.ContainsKey(forwardNeighborIndex))
            {
                Block neighbor = blocks[forwardNeighborIndex];
                block.ForwardNeighbor = neighbor;
                neighbor.BackwardNeighbor = block;
            }
            else
            {
                block.ForwardNeighbor = null;
            }

            // Right Forward
            string rightForwardNeighborIndex = $"{blockX + 1}{blockY}{blockZ + 1}";
            if (blocks.ContainsKey(rightForwardNeighborIndex))
            {
                Block neighbor = blocks[rightForwardNeighborIndex];
                block.RightForwardNeighbor = neighbor;
                neighbor.LeftBackwardNeighbor = block;
            }
            else
            {
                block.RightForwardNeighbor = null;
            }


            // Left
            string leftNeighborIndex = $"{blockX - 1}{blockY}{blockZ}";
            if (blocks.ContainsKey(leftNeighborIndex))
            {
                Block neighbor = blocks[leftNeighborIndex];
                block.LeftNeighbor = neighbor;
                neighbor.RightNeighbor = block;
            }
            else
            {
                block.LeftNeighbor = null;
            }

            // Right
            string rightNeighborIndex = $"{blockX + 1}{blockY}{blockZ}";
            if (blocks.ContainsKey(rightNeighborIndex))
            {
                Block neighbor = blocks[rightNeighborIndex];
                block.RightNeighbor = neighbor;
                neighbor.LeftNeighbor = block;
            }
            else
            {
                block.RightNeighbor = null;
            }


            // Left Backward
            string leftBackwardNeighborIndex = $"{blockX - 1}{blockY}{blockZ - 1}";
            if (blocks.ContainsKey(leftBackwardNeighborIndex))
            {
                Block neighbor = blocks[leftBackwardNeighborIndex];
                block.LeftBackwardNeighbor = neighbor;
                neighbor.RightForwardNeighbor = block;
            }
            else
            {
                block.LeftBackwardNeighbor = null;
            }

            // Backward
            string backwardNeighborIndex = $"{blockX}{blockY}{blockZ - 1}";
            if (blocks.ContainsKey(backwardNeighborIndex))
            {
                Block neighbor = blocks[backwardNeighborIndex];
                block.BackwardNeighbor = neighbor;
                neighbor.ForwardNeighbor = block;
            }
            else
            {
                block.BackwardNeighbor = null;
            }

            // Right Backward
            string rightBackwardNeighborIndex = $"{blockX + 1}{blockY}{blockZ - 1}";
            if (blocks.ContainsKey(rightBackwardNeighborIndex))
            {
                Block neighbor = blocks[rightBackwardNeighborIndex];
                block.RightBackwardNeighbor = neighbor;
                neighbor.LeftForwardNeighbor = block;
            }
            else
            {
                block.RightBackwardNeighbor = null;
            }


            // Bottom Left Forward
            string bottomLeftForwardNeighborIndex = $"{blockX - 1}{blockY - 1}{blockZ + 1}";
            if (blocks.ContainsKey(bottomLeftForwardNeighborIndex))
            {
                Block neighbor = blocks[bottomLeftForwardNeighborIndex];
                block.BottomLeftForwardNeighbor = neighbor;
                neighbor.TopRightBackwardNeighbor = block;
            }
            else
            {
                block.BottomLeftForwardNeighbor = null;
            }

            // Bottom Forward
            string bottomForwardNeighborIndex = $"{blockX}{blockY - 1}{blockZ + 1}";
            if (blocks.ContainsKey(bottomForwardNeighborIndex))
            {
                Block neighbor = blocks[bottomForwardNeighborIndex];
                block.BottomForwardNeighbor = neighbor;
                neighbor.TopBackwardNeighbor = block;
            }
            else
            {
                block.BottomForwardNeighbor = null;
            }

            // Bottom Right Forward
            string bottomRightForwardNeighborIndex = $"{blockX + 1}{blockY - 1}{blockZ + 1}";
            if (blocks.ContainsKey(bottomRightForwardNeighborIndex))
            {
                Block neighbor = blocks[bottomRightForwardNeighborIndex];
                block.BottomRightForwardNeighbor = neighbor;
                neighbor.TopLeftBackwardNeighbor = block;
            }
            else
            {
                block.BottomRightForwardNeighbor = null;
            }


            // Bottom Left
            string bottomLeftNeighborIndex = $"{blockX - 1}{blockY - 1}{blockZ}";
            if (blocks.ContainsKey(bottomLeftNeighborIndex))
            {
                Block neighbor = blocks[bottomLeftNeighborIndex];
                block.BottomLeftNeighbor = neighbor;
                neighbor.TopRightNeighbor = block;
            }
            else
            {
                block.BottomLeftNeighbor = null;
            }

            // Bottom
            string bottomNeighborIndex = $"{blockX}{blockY - 1}{blockZ}";
            if (blocks.ContainsKey(bottomNeighborIndex))
            {
                Block neighbor = blocks[bottomNeighborIndex];
                block.BottomNeighbor = neighbor;
                neighbor.TopNeighbor = block;
            }
            else
            {
                block.BottomNeighbor = null;
            }

            // Bottom Right
            string bottomRightNeighborIndex = $"{blockX + 1}{blockY - 1}{blockZ}";
            if (blocks.ContainsKey(bottomRightNeighborIndex))
            {
                Block neighbor = blocks[bottomRightNeighborIndex];
                block.BottomRightNeighbor = neighbor;
                neighbor.TopLeftNeighbor = block;
            }
            else
            {
                block.BottomRightNeighbor = null;
            }


            // Bottom Left Backward
            string bottomLeftBackwardNeighborIndex = $"{blockX - 1}{blockY - 1}{blockZ - 1}";
            if (blocks.ContainsKey(bottomLeftBackwardNeighborIndex))
            {
                Block neighbor = blocks[bottomLeftBackwardNeighborIndex];
                block.BottomLeftBackwardNeighbor = neighbor;
                neighbor.TopRightForwardNeighbor = block;
            }
            else
            {
                block.BottomLeftBackwardNeighbor = null;
            }

            // Bottom Backward
            string bottomBackwardNeighborIndex = $"{blockX}{blockY - 1}{blockZ - 1}";
            if (blocks.ContainsKey(bottomBackwardNeighborIndex))
            {
                Block neighbor = blocks[bottomBackwardNeighborIndex];
                block.BottomBackwardNeighbor = neighbor;
                neighbor.TopForwardNeighbor = block;
            }
            else
            {
                block.BottomBackwardNeighbor = null;
            }

            // Bottom Right Backward
            string bottomRightBackwardNeighborIndex = $"{blockX + 1}{blockY - 1}{blockZ - 1}";
            if (blocks.ContainsKey(bottomRightBackwardNeighborIndex))
            {
                Block neighbor = blocks[bottomRightBackwardNeighborIndex];
                block.BottomRightBackwardNeighbor = neighbor;
                neighbor.TopLeftForwardNeighbor = block;
            }
            else
            {
                block.BottomRightBackwardNeighbor = null;
            }
        }

        private void UpdateWalkableFacesByPosition(Block block)
        {
            float blockX = block.transform.position.x;
            float blockY = block.transform.position.y;
            float blockZ = block.transform.position.z;

            // Top
            block.Faces[0].walkable = blockY >= 0 ? true : false;

            // Bottom
            block.Faces[1].walkable = blockY <= 0 ? true : false;

            // Left
            block.Faces[2].walkable = blockX <= 0 ? true : false;

            // Right
            block.Faces[3].walkable = blockX >= 0 ? true : false;

            // Front
            block.Faces[4].walkable = blockZ >= 0 ? true : false;

            // Back
            block.Faces[5].walkable = blockZ <= 0 ? true : false;
        }

        private void UpdateWalkableFacesByNeighborhood(Block block)
        {
            if (block.TopNeighbor)
            {
                block.Faces[0].walkable = false;
                block.TopNeighbor.Faces[1].walkable = false;
            }
            if (block.BottomNeighbor)
            {
                block.Faces[1].walkable = false;
                block.BottomNeighbor.Faces[0].walkable = false;
            }
            if (block.LeftNeighbor)
            {
                block.Faces[2].walkable = false;
                block.LeftNeighbor.Faces[3].walkable = false;
            }
            if (block.RightNeighbor)
            {
                block.Faces[3].walkable = false;
                block.RightNeighbor.Faces[2].walkable = false;
            }
            if (block.ForwardNeighbor)
            {
                block.Faces[4].walkable = false;
                block.ForwardNeighbor.Faces[5].walkable = false;
            }
            if (block.BackwardNeighbor)
            {
                block.Faces[5].walkable = false;
                block.BackwardNeighbor.Faces[4].walkable = false;
            }
        }

        private void UpdateBlockFacesNeighborhood(Block block)
        {
            UpdateTopFaceNeighborhood(block);
            UpdateBottomFaceNeighborhood(block);
            UpdateLeftFaceNeighborhood(block);
            UpdateRightFaceNeighborhood(block);
            UpdateFrontFaceNeighborhood(block);
            UpdateBackFaceNeighborhood(block);
        }

        private void UpdateTopFaceNeighborhood(Block block)
        {
            Block topForwardNeighbor = block.TopForwardNeighbor;
            if (topForwardNeighbor)
            {
                block.Faces[0].Neighbors.Add(topForwardNeighbor.Faces[5]);
                topForwardNeighbor.Faces[5].Neighbors.Add(block.Faces[0]);
                block.Faces[0].Neighbors.Add(topForwardNeighbor.Faces[0]);
                topForwardNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block topBackwardNeighbor = block.TopBackwardNeighbor;
            if (topBackwardNeighbor)
            {
                block.Faces[0].Neighbors.Add(topBackwardNeighbor.Faces[4]);
                topBackwardNeighbor.Faces[4].Neighbors.Add(block.Faces[0]);
                block.Faces[0].Neighbors.Add(topBackwardNeighbor.Faces[0]);
                topBackwardNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block topLeftNeighbor = block.TopLeftNeighbor;
            if (topLeftNeighbor)
            {
                block.Faces[0].Neighbors.Add(topLeftNeighbor.Faces[3]);
                topLeftNeighbor.Faces[3].Neighbors.Add(block.Faces[0]);
                block.Faces[0].Neighbors.Add(topLeftNeighbor.Faces[0]);
                topLeftNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block topRightNeighbor = block.TopRightNeighbor;
            if (topRightNeighbor)
            {
                block.Faces[0].Neighbors.Add(topRightNeighbor.Faces[2]);
                topRightNeighbor.Faces[2].Neighbors.Add(block.Faces[0]);
                block.Faces[0].Neighbors.Add(topRightNeighbor.Faces[0]);
                topRightNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block forwardNeighbor = block.ForwardNeighbor;
            if (forwardNeighbor)
            {
                block.Faces[0].Neighbors.Add(forwardNeighbor.Faces[0]);
                forwardNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block backwardNeighbor = block.BackwardNeighbor;
            if (backwardNeighbor)
            {
                block.Faces[0].Neighbors.Add(backwardNeighbor.Faces[0]);
                backwardNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block leftNeighbor = block.LeftNeighbor;
            if (leftNeighbor)
            {
                block.Faces[0].Neighbors.Add(leftNeighbor.Faces[0]);
                leftNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block rightNeighbor = block.RightNeighbor;
            if (rightNeighbor)
            {
                block.Faces[0].Neighbors.Add(rightNeighbor.Faces[0]);
                rightNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block bottomForwardNeighbor = block.BottomForwardNeighbor;
            if (bottomForwardNeighbor)
            {
                block.Faces[0].Neighbors.Add(bottomForwardNeighbor.Faces[0]);
                bottomForwardNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block bottomBackwardNeighbor = block.BottomBackwardNeighbor;
            if (bottomBackwardNeighbor)
            {
                block.Faces[0].Neighbors.Add(bottomBackwardNeighbor.Faces[0]);
                bottomBackwardNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block bottomLeftNeighbor = block.BottomLeftNeighbor;
            if (bottomLeftNeighbor)
            {
                block.Faces[0].Neighbors.Add(bottomLeftNeighbor.Faces[0]);
                bottomLeftNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }

            Block bottomRightNeighbor = block.BottomRightNeighbor;
            if (bottomRightNeighbor)
            {
                block.Faces[0].Neighbors.Add(bottomRightNeighbor.Faces[0]);
                bottomRightNeighbor.Faces[0].Neighbors.Add(block.Faces[0]);
            }
        }

        private void UpdateBottomFaceNeighborhood(Block block)
        {
            Block topForwardNeighbor = block.TopForwardNeighbor;
            if (topForwardNeighbor)
            {
                block.Faces[1].Neighbors.Add(topForwardNeighbor.Faces[1]);
                topForwardNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block topBackwardNeighbor = block.TopBackwardNeighbor;
            if (topBackwardNeighbor)
            {
                block.Faces[1].Neighbors.Add(topBackwardNeighbor.Faces[1]);
                topBackwardNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block topLeftNeighbor = block.TopLeftNeighbor;
            if (topLeftNeighbor)
            {
                block.Faces[1].Neighbors.Add(topLeftNeighbor.Faces[1]);
                topLeftNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block topRightNeighbor = block.TopRightNeighbor;
            if (topRightNeighbor)
            {
                block.Faces[1].Neighbors.Add(topRightNeighbor.Faces[1]);
                topRightNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block forwardNeighbor = block.ForwardNeighbor;
            if (forwardNeighbor)
            {
                block.Faces[1].Neighbors.Add(forwardNeighbor.Faces[1]);
                forwardNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block backwardNeighbor = block.BackwardNeighbor;
            if (backwardNeighbor)
            {
                block.Faces[1].Neighbors.Add(backwardNeighbor.Faces[1]);
                backwardNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block leftNeighbor = block.LeftNeighbor;
            if (leftNeighbor)
            {
                block.Faces[1].Neighbors.Add(leftNeighbor.Faces[1]);
                leftNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block rightNeighbor = block.RightNeighbor;
            if (rightNeighbor)
            {
                block.Faces[1].Neighbors.Add(rightNeighbor.Faces[1]);
                rightNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block bottomForwardNeighbor = block.BottomForwardNeighbor;
            if (bottomForwardNeighbor)
            {
                block.Faces[1].Neighbors.Add(bottomForwardNeighbor.Faces[5]);
                bottomForwardNeighbor.Faces[5].Neighbors.Add(block.Faces[1]);
                block.Faces[1].Neighbors.Add(bottomForwardNeighbor.Faces[1]);
                bottomForwardNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block bottomBackwardNeighbor = block.BottomBackwardNeighbor;
            if (bottomBackwardNeighbor)
            {
                block.Faces[1].Neighbors.Add(bottomBackwardNeighbor.Faces[4]);
                bottomBackwardNeighbor.Faces[4].Neighbors.Add(block.Faces[1]);
                block.Faces[1].Neighbors.Add(bottomBackwardNeighbor.Faces[1]);
                bottomBackwardNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block bottomLeftNeighbor = block.BottomLeftNeighbor;
            if (bottomLeftNeighbor)
            {
                block.Faces[1].Neighbors.Add(bottomLeftNeighbor.Faces[3]);
                bottomLeftNeighbor.Faces[3].Neighbors.Add(block.Faces[1]);
                block.Faces[1].Neighbors.Add(bottomLeftNeighbor.Faces[1]);
                bottomLeftNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }

            Block bottomRightNeighbor = block.BottomRightNeighbor;
            if (bottomRightNeighbor)
            {
                block.Faces[1].Neighbors.Add(bottomRightNeighbor.Faces[2]);
                bottomRightNeighbor.Faces[2].Neighbors.Add(block.Faces[1]);
                block.Faces[1].Neighbors.Add(bottomRightNeighbor.Faces[1]);
                bottomRightNeighbor.Faces[1].Neighbors.Add(block.Faces[1]);
            }
        }

        private void UpdateLeftFaceNeighborhood(Block block)
        {
            Block topLeftNeighbor = block.TopLeftNeighbor;
            if (topLeftNeighbor)
            {
                block.Faces[2].Neighbors.Add(topLeftNeighbor.Faces[1]);
                topLeftNeighbor.Faces[1].Neighbors.Add(block.Faces[2]);
                block.Faces[2].Neighbors.Add(topLeftNeighbor.Faces[2]);
                topLeftNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block bottomLeftNeighbor = block.BottomLeftNeighbor;
            if (bottomLeftNeighbor)
            {
                block.Faces[2].Neighbors.Add(bottomLeftNeighbor.Faces[0]);
                bottomLeftNeighbor.Faces[0].Neighbors.Add(block.Faces[2]);
                block.Faces[2].Neighbors.Add(bottomLeftNeighbor.Faces[2]);
                bottomLeftNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block leftForwardNeighbor = block.LeftForwardNeighbor;
            if (leftForwardNeighbor)
            {
                block.Faces[2].Neighbors.Add(leftForwardNeighbor.Faces[5]);
                leftForwardNeighbor.Faces[5].Neighbors.Add(block.Faces[2]);
                block.Faces[2].Neighbors.Add(leftForwardNeighbor.Faces[2]);
                leftForwardNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block leftBackwardNeighbor = block.LeftBackwardNeighbor;
            if (leftBackwardNeighbor)
            {
                block.Faces[2].Neighbors.Add(leftBackwardNeighbor.Faces[4]);
                leftBackwardNeighbor.Faces[4].Neighbors.Add(block.Faces[2]);
                block.Faces[2].Neighbors.Add(leftBackwardNeighbor.Faces[2]);
                leftBackwardNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block topNeighbor = block.TopNeighbor;
            if (topNeighbor)
            {
                block.Faces[2].Neighbors.Add(topNeighbor.Faces[2]);
                topNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block bottomNeighbor = block.BottomNeighbor;
            if (bottomNeighbor)
            {
                block.Faces[2].Neighbors.Add(bottomNeighbor.Faces[2]);
                bottomNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block forwardNeighbor = block.ForwardNeighbor;
            if (forwardNeighbor)
            {
                block.Faces[2].Neighbors.Add(forwardNeighbor.Faces[2]);
                forwardNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block backwardNeighbor = block.BackwardNeighbor;
            if (backwardNeighbor)
            {
                block.Faces[2].Neighbors.Add(backwardNeighbor.Faces[2]);
                backwardNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block topRightNeighbor = block.TopRightNeighbor;
            if (topRightNeighbor)
            {
                block.Faces[2].Neighbors.Add(topRightNeighbor.Faces[2]);
                topRightNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block bottomRightNeighbor = block.BottomRightNeighbor;
            if (bottomRightNeighbor)
            {
                block.Faces[2].Neighbors.Add(bottomRightNeighbor.Faces[2]);
                bottomRightNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block rightForwardNeighbor = block.RightForwardNeighbor;
            if (rightForwardNeighbor)
            {
                block.Faces[2].Neighbors.Add(rightForwardNeighbor.Faces[2]);
                rightForwardNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }

            Block rightBackwardNeighbor = block.RightBackwardNeighbor;
            if (rightBackwardNeighbor)
            {
                block.Faces[2].Neighbors.Add(rightBackwardNeighbor.Faces[2]);
                rightBackwardNeighbor.Faces[2].Neighbors.Add(block.Faces[2]);
            }
        }

        private void UpdateRightFaceNeighborhood(Block block)
        {
            Block topLeftNeighbor = block.TopLeftNeighbor;
            if (topLeftNeighbor)
            {
                block.Faces[3].Neighbors.Add(topLeftNeighbor.Faces[3]);
                topLeftNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block bottomLeftNeighbor = block.BottomLeftNeighbor;
            if (bottomLeftNeighbor)
            {
                block.Faces[3].Neighbors.Add(bottomLeftNeighbor.Faces[3]);
                bottomLeftNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block leftForwardNeighbor = block.LeftForwardNeighbor;
            if (leftForwardNeighbor)
            {
                block.Faces[3].Neighbors.Add(leftForwardNeighbor.Faces[3]);
                leftForwardNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block leftBackwardNeighbor = block.LeftBackwardNeighbor;
            if (leftBackwardNeighbor)
            {
                block.Faces[3].Neighbors.Add(leftBackwardNeighbor.Faces[3]);
                leftBackwardNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block topNeighbor = block.TopNeighbor;
            if (topNeighbor)
            {
                block.Faces[3].Neighbors.Add(topNeighbor.Faces[3]);
                topNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block bottomNeighbor = block.BottomNeighbor;
            if (bottomNeighbor)
            {
                block.Faces[3].Neighbors.Add(bottomNeighbor.Faces[3]);
                bottomNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block forwardNeighbor = block.ForwardNeighbor;
            if (forwardNeighbor)
            {
                block.Faces[3].Neighbors.Add(forwardNeighbor.Faces[3]);
                forwardNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block backwardNeighbor = block.BackwardNeighbor;
            if (backwardNeighbor)
            {
                block.Faces[3].Neighbors.Add(backwardNeighbor.Faces[3]);
                backwardNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block topRightNeighbor = block.TopRightNeighbor;
            if (topRightNeighbor)
            {
                block.Faces[3].Neighbors.Add(topRightNeighbor.Faces[1]);
                topRightNeighbor.Faces[1].Neighbors.Add(block.Faces[3]);
                block.Faces[3].Neighbors.Add(topRightNeighbor.Faces[3]);
                topRightNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block bottomRightNeighbor = block.BottomRightNeighbor;
            if (bottomRightNeighbor)
            {
                block.Faces[3].Neighbors.Add(bottomRightNeighbor.Faces[0]);
                bottomRightNeighbor.Faces[0].Neighbors.Add(block.Faces[3]);
                block.Faces[3].Neighbors.Add(bottomRightNeighbor.Faces[3]);
                bottomRightNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block rightForwardNeighbor = block.RightForwardNeighbor;
            if (rightForwardNeighbor)
            {
                block.Faces[3].Neighbors.Add(rightForwardNeighbor.Faces[5]);
                rightForwardNeighbor.Faces[5].Neighbors.Add(block.Faces[3]);
                block.Faces[3].Neighbors.Add(rightForwardNeighbor.Faces[3]);
                rightForwardNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }

            Block rightBackwardNeighbor = block.RightBackwardNeighbor;
            if (rightBackwardNeighbor)
            {
                block.Faces[3].Neighbors.Add(rightBackwardNeighbor.Faces[4]);
                rightBackwardNeighbor.Faces[4].Neighbors.Add(block.Faces[3]);
                block.Faces[3].Neighbors.Add(rightBackwardNeighbor.Faces[3]);
                rightBackwardNeighbor.Faces[3].Neighbors.Add(block.Faces[3]);
            }
        }

        private void UpdateFrontFaceNeighborhood(Block block)
        {
            Block topForwardNeighbor = block.TopForwardNeighbor;
            if (topForwardNeighbor)
            {
                block.Faces[4].Neighbors.Add(topForwardNeighbor.Faces[1]);
                topForwardNeighbor.Faces[1].Neighbors.Add(block.Faces[4]);
                block.Faces[4].Neighbors.Add(topForwardNeighbor.Faces[4]);
                topForwardNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block bottomForwardNeighbor = block.BottomForwardNeighbor;
            if (bottomForwardNeighbor)
            {
                block.Faces[4].Neighbors.Add(bottomForwardNeighbor.Faces[0]);
                bottomForwardNeighbor.Faces[0].Neighbors.Add(block.Faces[4]);
                block.Faces[4].Neighbors.Add(bottomForwardNeighbor.Faces[4]);
                bottomForwardNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block leftForwardNeighbor = block.LeftForwardNeighbor;
            if (leftForwardNeighbor)
            {
                block.Faces[4].Neighbors.Add(leftForwardNeighbor.Faces[3]);
                leftForwardNeighbor.Faces[3].Neighbors.Add(block.Faces[4]);
                block.Faces[4].Neighbors.Add(leftForwardNeighbor.Faces[4]);
                leftForwardNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block rightForwardNeighbor = block.RightForwardNeighbor;
            if (rightForwardNeighbor)
            {
                block.Faces[4].Neighbors.Add(rightForwardNeighbor.Faces[2]);
                rightForwardNeighbor.Faces[2].Neighbors.Add(block.Faces[4]);
                block.Faces[4].Neighbors.Add(rightForwardNeighbor.Faces[4]);
                rightForwardNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block topNeighbor = block.TopNeighbor;
            if (topNeighbor)
            {
                block.Faces[4].Neighbors.Add(topNeighbor.Faces[4]);
                topNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block bottomNeighbor = block.BottomNeighbor;
            if (bottomNeighbor)
            {
                block.Faces[4].Neighbors.Add(bottomNeighbor.Faces[4]);
                bottomNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block leftNeighbor = block.LeftNeighbor;
            if (leftNeighbor)
            {
                block.Faces[4].Neighbors.Add(leftNeighbor.Faces[4]);
                leftNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block rightNeighbor = block.RightNeighbor;
            if (rightNeighbor)
            {
                block.Faces[4].Neighbors.Add(rightNeighbor.Faces[4]);
                rightNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block topBackwardNeighbor = block.TopBackwardNeighbor;
            if (topBackwardNeighbor)
            {
                block.Faces[4].Neighbors.Add(topBackwardNeighbor.Faces[4]);
                topBackwardNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block bottomBackwardNeighbor = block.BottomBackwardNeighbor;
            if (bottomBackwardNeighbor)
            {
                block.Faces[4].Neighbors.Add(bottomBackwardNeighbor.Faces[4]);
                bottomBackwardNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block leftBackwardNeighbor = block.LeftBackwardNeighbor;
            if (leftBackwardNeighbor)
            {
                block.Faces[4].Neighbors.Add(leftBackwardNeighbor.Faces[4]);
                leftBackwardNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }

            Block rightBackwardNeighbor = block.RightBackwardNeighbor;
            if (rightBackwardNeighbor)
            {
                block.Faces[4].Neighbors.Add(rightBackwardNeighbor.Faces[4]);
                rightBackwardNeighbor.Faces[4].Neighbors.Add(block.Faces[4]);
            }
        }

        private void UpdateBackFaceNeighborhood(Block block)
        {
            Block topForwardNeighbor = block.TopForwardNeighbor;
            if (topForwardNeighbor)
            {
                block.Faces[5].Neighbors.Add(topForwardNeighbor.Faces[5]);
                topForwardNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block bottomForwardNeighbor = block.BottomForwardNeighbor;
            if (bottomForwardNeighbor)
            {
                block.Faces[5].Neighbors.Add(bottomForwardNeighbor.Faces[5]);
                bottomForwardNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block leftForwardNeighbor = block.LeftForwardNeighbor;
            if (leftForwardNeighbor)
            {
                block.Faces[5].Neighbors.Add(leftForwardNeighbor.Faces[5]);
                leftForwardNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block rightForwardNeighbor = block.RightForwardNeighbor;
            if (rightForwardNeighbor)
            {
                block.Faces[5].Neighbors.Add(rightForwardNeighbor.Faces[5]);
                rightForwardNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block topNeighbor = block.TopNeighbor;
            if (topNeighbor)
            {
                block.Faces[5].Neighbors.Add(topNeighbor.Faces[5]);
                topNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block bottomNeighbor = block.BottomNeighbor;
            if (bottomNeighbor)
            {
                block.Faces[5].Neighbors.Add(bottomNeighbor.Faces[5]);
                bottomNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block leftNeighbor = block.LeftNeighbor;
            if (leftNeighbor)
            {
                block.Faces[5].Neighbors.Add(leftNeighbor.Faces[5]);
                leftNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block rightNeighbor = block.RightNeighbor;
            if (rightNeighbor)
            {
                block.Faces[5].Neighbors.Add(rightNeighbor.Faces[5]);
                rightNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block topBackwardNeighbor = block.TopBackwardNeighbor;
            if (topBackwardNeighbor)
            {
                block.Faces[5].Neighbors.Add(topBackwardNeighbor.Faces[1]);
                topBackwardNeighbor.Faces[1].Neighbors.Add(block.Faces[5]);
                block.Faces[5].Neighbors.Add(topBackwardNeighbor.Faces[5]);
                topBackwardNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block bottomBackwardNeighbor = block.BottomBackwardNeighbor;
            if (bottomBackwardNeighbor)
            {
                block.Faces[5].Neighbors.Add(bottomBackwardNeighbor.Faces[0]);
                bottomBackwardNeighbor.Faces[0].Neighbors.Add(block.Faces[5]);
                block.Faces[5].Neighbors.Add(bottomBackwardNeighbor.Faces[5]);
                bottomBackwardNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block leftBackwardNeighbor = block.LeftBackwardNeighbor;
            if (leftBackwardNeighbor)
            {
                block.Faces[5].Neighbors.Add(leftBackwardNeighbor.Faces[3]);
                leftBackwardNeighbor.Faces[3].Neighbors.Add(block.Faces[5]);
                block.Faces[5].Neighbors.Add(leftBackwardNeighbor.Faces[5]);
                leftBackwardNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }

            Block rightBackwardNeighbor = block.RightBackwardNeighbor;
            if (rightBackwardNeighbor)
            {
                block.Faces[5].Neighbors.Add(rightBackwardNeighbor.Faces[2]);
                rightBackwardNeighbor.Faces[2].Neighbors.Add(block.Faces[5]);
                block.Faces[5].Neighbors.Add(rightBackwardNeighbor.Faces[5]);
                rightBackwardNeighbor.Faces[5].Neighbors.Add(block.Faces[5]);
            }
        }

        //private void UpdateBlockNeighbors(Block block)
        //{
        //    block.ClearNeighbors();
        //    float blockX = block.transform.position.x;
        //    float blockY = block.transform.position.y;
        //    float blockZ = block.transform.position.z;
        //    for(int x = -1; x < 2; x++)
        //    {
        //        for (int y = -1; y < 2; y++)
        //        {
        //            for (int z = -1; z < 2; z++)
        //            {
        //                if(x == 0 && y == 0 && z == 0) { continue; }
        //                string neighborIndexString = $"{blockX + x}{blockY + y}{blockZ + z}";
        //                if (blocks.ContainsKey(neighborIndexString))
        //                {
        //                    Block neighbor = blocks[neighborIndexString];
        //                    block.AddNeighbor(neighbor);
        //                    neighbor.AddNeighbor(block);
        //                }
        //            }
        //        }
        //    }
        //}

        public void SetFacesDebugMode(bool active)
        {
            foreach(var keyValuePair in blocks)
            {
                Block block = blocks[keyValuePair.Key];
                foreach(var face in block.Faces)
                {
                    face.SetDebugMode(active);
                }
            }
        }

        public void SetHoveredBlockFace(BlockFace blockFace)
        {
            currentHoveredBlockFace = blockFace;
        }

        public void ShowFaceNeighbors()
        {
            if (currentSelectedBlockFace)
            {
                currentSelectedBlockFace.ShowNeighbors(false);
            }
            currentSelectedBlockFace = currentHoveredBlockFace;
            if (currentSelectedBlockFace)
            {
                currentSelectedBlockFace.ShowNeighbors(true);
            }
        }
    }
}
