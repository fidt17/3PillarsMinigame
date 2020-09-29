using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
    private const short _gridSize = 5;

    private Cell[,] _cells;

    public Cell CellAt(int x, int y) => (IsPositionViable(x, y)) ? _cells[x, y] : null;

    public void CreateGrid() {
        CreateCells();
        CreateBlocks();
    }

    public void ShuffleBlocks() {
        List<Block> existingBlocks = new List<Block>();
        for (int x = 0; x < _gridSize; x++) {
            for (int y = 0; y < _gridSize; y++) {
                if (_cells[x, y].HasBlock && _cells[x, y].Block.Type != BlockType.solid) {
                    existingBlocks.Add(_cells[x, y].Block);
                    _cells[x, y].RemoveBlock();
                }
            }
        }

        foreach (Block block in existingBlocks) {
            Cell cell = GetRandomEmptyCell();
            block.SetPosition(cell.Position);
            cell.AddBlock(block);
        }
    }

    private void CreateCells() {
        _cells = new Cell[_gridSize, _gridSize];
        for (int x = 0; x < _gridSize; x++) {
            for (int y = 0; y < _gridSize; y++) {
                GameObject obj = Factory.GetInstance().CreateCell(x, y);
                _cells[x, y] = new Cell(new Vector2Int(x, y), obj);
            }
        }
    }

    private void CreateBlocks() {
        //Solid blocks
        for (int x = 1; x <= 3; x += 2) {
            for (int y = 0; y <= 4; y += 2) {
                GameObject obj = Factory.GetInstance().CreateBlockOfType(BlockType.solid);
                Vector2Int position = new Vector2Int(x, y);
                Block block = new Block(position, BlockType.solid, obj);
                _cells[x, y].AddBlock(block);
            }
        }
        //Draggable blocks
        for (int blockColumn = 0; blockColumn < 3; blockColumn++) {
            for (int i = 0; i < 5; i++) {
                GameObject obj = Factory.GetInstance().CreateBlockOfType((BlockType) blockColumn);
                Cell cell = GetRandomEmptyCell();
                Block block = new Block(cell.Position, (BlockType) blockColumn, obj);
                cell.AddBlock(block);
            }
        }
    }

    private Cell GetRandomEmptyCell() {
        Cell cell;
        do {
            int x = (int) Random.Range(0, 5);
            int y = (int) Random.Range(0, 5);
            cell = _cells[x, y];
        } while (cell.HasBlock != false);
        return cell;
    }

    private bool IsPositionViable(int x, int y) => (x >= 0 && x < _gridSize && y >= 0 && y < _gridSize);
}
