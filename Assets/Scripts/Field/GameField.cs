using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameField : Singleton<GameField> {

    public FieldData Data => _fieldData;

    [SerializeField] private List<GameObject> _rowIndicators;
    [SerializeField] private GameObject[,] _cells = new GameObject[5, 5];

    private FieldData _fieldData;

    private void Awake() {
        _fieldData = new FieldData();
    }

    public void ChangeRowColorAt(int index, Color newColor) {
        if (index < 0 || index >= _rowIndicators.Count) {
            Debug.LogError("IndexOutOfRange", this);
            return;
        }
        _rowIndicators[index].GetComponent<SpriteRenderer>().color = newColor;
    }

    public void AddCellAt(Vector2Int position, GameObject cell) {
        if (position.x < 0 || position.x >= 5 || position.y < 0 || position.y >= 5) {
            Destroy(cell);
            Debug.LogError("IndexOutOfRange", this);
            return;
        }
        _cells[position.x, position.y] = cell;
    }

    public List<CellComponent> GetPossibleCellsForBlock(Block block) {
        List<CellComponent> cells = new List<CellComponent>();
        Vector2Int checkPosition = block.Position;

        cells.Add(_cells[block.X, block.Y].GetComponent<CellComponent>());
        
        checkPosition = block.Position - Vector2Int.right;
        if (IsPositionViable(checkPosition) && Data.BlockAt(checkPosition) is null) {
            cells.Add(_cells[checkPosition.x, checkPosition.y].GetComponent<CellComponent>());
        }

        checkPosition = block.Position + Vector2Int.right;
        if (IsPositionViable(checkPosition) && Data.BlockAt(checkPosition) is null) {
            cells.Add(_cells[checkPosition.x, checkPosition.y].GetComponent<CellComponent>());
        }

        checkPosition = block.Position - Vector2Int.up;
        if (IsPositionViable(checkPosition) && Data.BlockAt(checkPosition) is null) {
            cells.Add(_cells[checkPosition.x, checkPosition.y].GetComponent<CellComponent>());
        }

        checkPosition = block.Position + Vector2Int.up;
        if (IsPositionViable(checkPosition) && Data.BlockAt(checkPosition) is null) {
            cells.Add(_cells[checkPosition.x, checkPosition.y].GetComponent<CellComponent>());
        }

        return cells;
    }

    public void CheckWinCondition() {
        int x = 0;
        for (int y = 0; y < 5; y++) {
            if (Data.BlockAt(new Vector2Int(x, y))?.Type != BlockType.type0)
                return; 
        }

        x = 2;
        for (int y = 0; y < 5; y++) {
            if (Data.BlockAt(new Vector2Int(x, y))?.Type != BlockType.type1)
                return; 
        }

        x = 4;
        for (int y = 0; y < 5; y++) {
            if (Data.BlockAt(new Vector2Int(x, y))?.Type != BlockType.type2)
                return; 
        }

        UIManager.GetInstance().ShowWinMenu();
    }

    public bool IsPositionViable(Vector2Int position) {
        if (position.x < 0 || position.x >= 5 || position.y < 0 || position.y >= 5) {
            return false;
        }
        return true;
    }

    public bool IsPositionViable(int x, int y) {
        if (x < 0 || x > 5 || y < 0 || y > 5) {
            return false;
        }
        return true;
    }
}