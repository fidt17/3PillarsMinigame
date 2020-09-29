using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameField : Singleton<GameField> {

    private Grid _grid;

    private void Start() => _grid = new Grid();

    public void StartNewGame() => _grid.CreateGrid();
    public void RestartGame()  => _grid.ShuffleBlocks();

    public void LiftBlock(Block block) => _grid.CellAt(block.X, block.Y)?.RemoveBlock();
    public void PutBlock(Block block)  => _grid.CellAt(block.X, block.Y)?.AddBlock(block);

    public void CheckWinCondition() {
        for (int x = 0; x < 5; x += 2) {
            BlockType columnType = (BlockType) (x / 2);
            for (int y = 0; y < 5; y++) {
                if (_grid.CellAt(x, y).Block?.Type != columnType) {
                    return;
                }
            }
        }
        UIManager.GetInstance().ShowWinMenu();
    }

    public List<CellComponent> GetEmptyAdjacentCells(Block block) {
        List<CellComponent> cells = new List<CellComponent>();
        Vector2Int checkPosition = block.Position;
        Cell checkCell = _grid.CellAt(checkPosition.x, checkPosition.y);
        //middle
        if (checkCell?.HasBlock == false) {
            cells.Add(checkCell.CellComponent);
        }
        //west
        checkPosition = block.Position - Vector2Int.right;
        checkCell = _grid.CellAt(checkPosition.x, checkPosition.y);
        if (checkCell?.HasBlock == false) {
            cells.Add(checkCell.CellComponent);
        }
        //east
        checkPosition = block.Position + Vector2Int.right;
        checkCell = _grid.CellAt(checkPosition.x, checkPosition.y);
        if (checkCell?.HasBlock == false) {
            cells.Add(checkCell.CellComponent);
        }
        //south
        checkPosition = block.Position - Vector2Int.up;
        checkCell = _grid.CellAt(checkPosition.x, checkPosition.y);
        if (checkCell?.HasBlock == false) {
            cells.Add(checkCell.CellComponent);
        }
        //north
        checkPosition = block.Position + Vector2Int.up;
        checkCell = _grid.CellAt(checkPosition.x, checkPosition.y);
        if (checkCell?.HasBlock == false) {
            cells.Add(checkCell.CellComponent);
        }
        return cells;
    }
}