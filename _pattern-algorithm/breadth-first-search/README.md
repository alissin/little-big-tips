## _**Little Big Tips**_ ![Joystick](https://raw.githubusercontent.com/alissin/alissin.github.io/master/images/joystick.png) > Pattern / Algorithm

### pathfinder using Breadth First Search

Based on this demonstration / prototype: [Realm Defender](https://simmer.io/@alissin/realm-defender)

> ![Realm Defender](https://raw.githubusercontent.com/alissin/alissin.github.io/master/demonstration-projects/realm-defender.png)

#### Scenario
Strong enemy warriors are spawning from enemy base (red castle) and going to attack the player base (blue castle).

#### Problem description
Find the shortest path between start point (red castle) and end point (blue castle).

#### Solution simplified concept
The technique of Breadth First Search consists in, given a start point, it looks for the neighbour points (blocks in this case), it sets on each of these blocks where the search comes from (I like to refer this as "tail") and finally, once the algorithm finds the end point, it reads each block backwards (starting with the end point), check the "tail" and create a (reverse) list with it.

#### Solution suggestion
In this case, the path will not change in runtime. When the level starts, the shortest path should already be defined and the enemy warriors should have access to it.

In the hierarchy, create a game object and put all the blocks nested (as a child) to this game object. Name this as `Level`:

```
Hierarchy:
- Level
-- Block 1
-- Block 2
...
-- Block N
```

 Create a C# script `Level.cs` and attach this script to the `Level` game object:

```csharp
public class Level : MonoBehaviour {
    ...
```

Define the fields:<br/>
P.S.: you can find the `Block.cs` script in the repository.

```csharp
[SerializeField]
Block _startBlock;

[SerializeField]
Block _endBlock;

Dictionary<Vector2Int, Block> _gridDic = new Dictionary<Vector2Int, Block>();
Queue<Block> _queue = new Queue<Block>();

Block[] _shortestPathBlocks;

// the order directions of the search, you can change it if you want
Vector2Int[] _directions = {
    Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left
};
```

When your level is loaded, start the process.

Step 1 - get all blocks in your level and put it in the `Dictionary`:

```csharp
void LoadLevelBlocks() {
    Block[] levelBlocks = transform.GetComponentsInChildren<Block>();

    foreach (var item in levelBlocks) {
        Vector2Int gridPos = item.GetGridPosition();
        if (!_gridDic.ContainsKey(gridPos)) {
            _gridDic.Add(gridPos, item);
        }
    }
}
```

Step 2 - find the neighbour blocks, given the center search point. The `Queue` and the `Dictionary` will help to organize it:

```csharp
void FindPathBlocksBFS() {
    _startBlock.IsEnqueued = true;
    _queue.Enqueue(_startBlock);

    while (_queue.Count > 0) {
        var centerSearchBlock = _queue.Dequeue();

        // check if the algorithm already found the end point
        if (centerSearchBlock.GetGridPosition() == _endBlock.GetGridPosition()) {
            break;
        }

        FindNeighbourBlocks(centerSearchBlock);
    }
}

void FindNeighbourBlocks(Block centerSearchBlock) {
    foreach (var item in _directions) {
        Vector2Int neighbourPos = centerSearchBlock.GetGridPosition() + item;

        Block neighbourBlock;
        if (_gridDic.TryGetValue(neighbourPos, out neighbourBlock)) {
            if (!neighbourBlock.IsEnqueued) {
                // set the tail (where the search comes from)
                neighbourBlock.Tail = centerSearchBlock;
                // enqueue the block to be able to read it (previous method)
                neighbourBlock.IsEnqueued = true;
                _queue.Enqueue(neighbourBlock);
            }
        }
    }
}
```

Step 3 - finally, let's create our shortest path:

```csharp
void BuildShortestPathBlocks() {
    // create a temporary reverse list and starts it with the end point
    List<Block> reversePathBlocks = new List<Block>();
    reversePathBlocks.Add(_endBlock);

    // after that, put every tail in the list and check until it finds the start point
    Block previousBlock = _endBlock.Tail;
    while (previousBlock != null) {
        reversePathBlocks.Add(previousBlock);

        if (previousBlock.GetGridPosition() == _startBlock.GetGridPosition()) {
            break;
        }

        previousBlock = previousBlock.Tail;
    }

    // set the size of the final array based on reverse list size, fill it in a reverse mode and here we go: our shortest path blocks!
    _shortestPathBlocks = new Block[reversePathBlocks.Count];
    for (int i = reversePathBlocks.Count - 1; i >= 0; i--) {
        _shortestPathBlocks[reversePathBlocks.Count - 1 - i] = reversePathBlocks[i];
    }
}
```

#### Scripts:
[Block.cs](./Block.cs), [Level.cs](./Level.cs)

More _**Little Big Tips**_? Nice, [follow me](https://github.com/alissin/little-big-tips)!

Are you done? Want some fun? No problem, let's [play](https://simmer.io/@alissin)!