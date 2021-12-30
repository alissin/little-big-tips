using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    Block startBlock;

    [SerializeField]
    Block endBlock;

    Dictionary<Vector2Int, Block> gridDic = new Dictionary<Vector2Int, Block>();
    Queue<Block> queue = new Queue<Block>();

    Block[] shortestPathBlocks;

    // the order directions of the search, you can change it if you want
    Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    // TODO: when your level is loaded, start the process with this method
    void LoadLevelBlocks()
    {
        Block[] levelBlocks = transform.GetComponentsInChildren<Block>();

        foreach (var item in levelBlocks)
        {
            Vector2Int gridPos = item.GetGridPosition();
            if (!gridDic.ContainsKey(gridPos))
            {
                gridDic.Add(gridPos, item);
            }
        }
    }

    void FindPathBlocksBFS()
    {
        startBlock.IsEnqueued = true;
        queue.Enqueue(startBlock);

        while (queue.Count > 0)
        {
            var centerSearchBlock = queue.Dequeue();

            // check if the algorithm already found the end point
            if (centerSearchBlock.GetGridPosition() == endBlock.GetGridPosition())
            {
                break;
            }

            FindNeighbourBlocks(centerSearchBlock);
        }
    }

    void FindNeighbourBlocks(Block centerSearchBlock)
    {
        foreach (var item in directions)
        {
            Vector2Int neighbourPos = centerSearchBlock.GetGridPosition() + item;

            Block neighbourBlock;
            if (gridDic.TryGetValue(neighbourPos, out neighbourBlock))
            {
                if (!neighbourBlock.IsEnqueued)
                {
                    // set the tail (where the search comes from)
                    neighbourBlock.Tail = centerSearchBlock;
                    // enqueue the block to be able to read it (previous method)
                    neighbourBlock.IsEnqueued = true;
                    queue.Enqueue(neighbourBlock);
                }
            }
        }
    }

    void BuildShortestPathBlocks()
    {
        // create a temporary reverse list and starts it with the end point
        List<Block> reversePathBlocks = new List<Block>();
        reversePathBlocks.Add(endBlock);

        // after that, put every tail in the list and check until it finds the start point
        Block previousBlock = endBlock.Tail;
        while (previousBlock != null)
        {
            reversePathBlocks.Add(previousBlock);

            if (previousBlock.GetGridPosition() == startBlock.GetGridPosition())
            {
                break;
            }

            previousBlock = previousBlock.Tail;
        }

        // set the size of the final array based on reverse list size, fill it in a reverse mode and here we go: our shortest path blocks!
        shortestPathBlocks = new Block[reversePathBlocks.Count];
        for (int i = reversePathBlocks.Count - 1; i >= 0; i--)
        {
            shortestPathBlocks[reversePathBlocks.Count - 1 - i] = reversePathBlocks[i];
        }
    }
}