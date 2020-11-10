using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameField : Singleton<GameField> {

    [SerializeField] private List<RowIndicatorScript> _indicators = new List<RowIndicatorScript>();

    private Grid _grid;

    private void Start() => _grid = new Grid();

    public Cell CellAt(Vector2Int position) => _grid.CellAt(position.x, position.y);

    public void StartNewGame() => _grid.CreateGrid();
    public void RestartGame()  => _grid.ShuffleBlocks(_grid.GetDraggableBlocks());

    public void LiftBlock(Block block) => _grid.CellAt(block.X, block.Y)?.RemoveBlock();
    public void PutBlockAt(Block block, Cell cell)  => cell?.AddBlock(block);

    public bool CheckWinCondition() {
        int rowIndex = 0;
        bool result = true;
        for (int x = 0; x < 5; x += 2) {
            BlockType columnType = (BlockType) (x / 2);
            int blockCount = 0;
            for (int y = 0; y < 5; y++) {
                if (_grid.CellAt(x, y).Block?.Type != columnType) {
                    UpdateIndicator(_indicators[rowIndex], blockCount);
                    result = false;
                } else {
                    blockCount++;
                }
            }
            UpdateIndicator(_indicators[rowIndex], blockCount);
            rowIndex++;
        }
        return result;
    }

    private void UpdateIndicator(RowIndicatorScript indicator, int blockCount) {
        indicator.SetSprite(blockCount);
    }

    public List<Cell> GetEmptyAdjacentCells(Block block) {
        List<Cell> openSet = new List<Cell>();
        List<Cell> closedSet = new List<Cell>();
        openSet.Add(_grid.CellAt(block.X, block.Y));

        do {
        } while (NextIteration(ref openSet, ref closedSet));
        return closedSet;
    }

    private bool NextIteration(ref List<Cell> openSet, ref List<Cell> closedSet) {
        if (openSet.Count == 0) {
            return false;
        }

        Cell currentCell = openSet[0];
        openSet.Remove(currentCell);
        closedSet.Add(currentCell);

        foreach (var cell in currentCell.GetAdjacentCells()) {
            if (cell.HasBlock || openSet.Contains(cell) || closedSet.Contains(cell)) {
                continue;
            }
            openSet.Add(cell);
        }

        return true;
    }
}