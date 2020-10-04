using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameField : Singleton<GameField> {

    private Grid _grid;

    private void Start() => _grid = new Grid();

    public void StartNewGame() => _grid.CreateGrid();
    public void RestartGame()  => _grid.ShuffleBlocks(_grid.GetDraggableBlocks());

    public void LiftBlock(Block block) => _grid.CellAt(block.X, block.Y)?.RemoveBlock();
    public void PutBlockAt(Block block, Cell cell)  => cell?.AddBlock(block);

    public bool CheckWinCondition() {
        for (int x = 0; x < 5; x += 2) {
            BlockType columnType = (BlockType) (x / 2);
            for (int y = 0; y < 5; y++) {
                if (_grid.CellAt(x, y).Block?.Type != columnType) {
                    return false;
                }
            }
        }
        return true;
    }

    public List<Cell> GetEmptyAdjacentCells(Block block) {
        List<Cell> cells = new List<Cell>();
        List<Vector2Int> checkPositions = new List<Vector2Int>();
        checkPositions.Add(block.Position);
        checkPositions.Add(block.Position - Vector2Int.right);
        checkPositions.Add(block.Position + Vector2Int.right);
        checkPositions.Add(block.Position - Vector2Int.up);
        checkPositions.Add(block.Position + Vector2Int.up);

        foreach (Vector2Int checkPosition in checkPositions) {
            Cell checkCell = _grid.CellAt(checkPosition.x, checkPosition.y);
            if (checkCell?.HasBlock == false) {
                cells.Add(checkCell);
            }
        }

        return cells;
    }
}