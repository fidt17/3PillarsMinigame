using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldData {

    private const short _fieldSize = 5;
    
    private Block[,] blocks = new Block[_fieldSize, _fieldSize];

    public void AddBlock(Block block) => blocks[block.X, block.Y] = block;
    public void RemoveBlock(Block block) => blocks[block.X, block.Y] = null;
    
    public Block BlockAt(Vector2Int position) {
        if (position.x < 0 || position.x >= _fieldSize || position.y < 0 || position.y >= _fieldSize) {
            return null;
        }
        return blocks[position.x, position.y];
    }

    public void DeleteBlocks() {
        for (int x = 0; x < _fieldSize; x++) {
            for (int y = 0; y < _fieldSize; y++) {
                Block block = blocks[x,y];
                block?.Destroy();
            }
        }

        blocks = new Block[_fieldSize, _fieldSize];
    }
}