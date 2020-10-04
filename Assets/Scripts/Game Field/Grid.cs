using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid {

    private const int _gridSize = 5;

    private Cell[,] _cells;

    public void CreateGrid() {
        CreateCells();
        CreateBlocks();
    }

    public Cell CellAt(int x, int y) => (IsPositionViable(x, y)) ? _cells[x, y] : null;

    //Although the chance of win condition after shuffling is extremly low,
    //I will put a check here anyway.
    public void ShuffleBlocks(List<Block> draggableBlocks) {
        while (draggableBlocks != null) {
            draggableBlocks.ForEach(x => GetRandomEmptyCell().AddBlock(x));
            draggableBlocks = (GameField.GetInstance().CheckWinCondition()) ? GetDraggableBlocks() : null;
        }
    }

    public List<Block> GetDraggableBlocks() {
        List<Block> draggableBlocks = new List<Block>();
        for (int x = 0; x < _gridSize; x++) {
            for (int y = 0; y < _gridSize; y++) {
                if (_cells[x, y].HasBlock && _cells[x, y].Block.Type != BlockType.solid) {
                    draggableBlocks.Add(_cells[x, y].Block);
                    _cells[x, y].RemoveBlock();
                }
            }
        }

        return draggableBlocks;
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
                _cells[x, y].AddBlock(new Block(BlockType.solid, obj));
            }
        }

        List<Block> draggableBlocks = new List<Block>();
        for (int blockColumn = 0; blockColumn < 3; blockColumn++) {
            for (int i = 0; i < 5; i++) {
                GameObject obj = Factory.GetInstance().CreateBlockOfType((BlockType) blockColumn);
                draggableBlocks.Add(new Block((BlockType) blockColumn, obj));
            }
        }
        ShuffleBlocks(draggableBlocks);
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