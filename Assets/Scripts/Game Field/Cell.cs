using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell {
    
    public Vector2Int Position => _position;
    public int X => _position.x;
    public int Y => _position.y;

    public Block Block => _block;
    public bool HasBlock => _block != null;
    public GameObject GameObject => _gameObject;

    public CellComponent CellComponent => _gameObject.GetComponent<CellComponent>();

    private Vector2Int _position;
    private GameObject _gameObject;
    private Block _block;

    public Cell(Vector2Int position, GameObject gameObject) {
        _position = position;
        _gameObject = gameObject;
    }

    public void AddBlock(Block block) {
        if (_block != null) {
            Debug.LogError("Cell already has a block.");
            return;
        }
        _block = block;
        _block.SetPosition(_position);
    }

    public void RemoveBlock() => _block = null;

    public List<Cell> GetAdjacentCells() {
        List<Cell> neighbours = new List<Cell>();
        List<Vector2Int> checkPositions = new List<Vector2Int>();
        checkPositions.Add(Position - Vector2Int.right);
        checkPositions.Add(Position + Vector2Int.right);
        checkPositions.Add(Position - Vector2Int.up);
        checkPositions.Add(Position + Vector2Int.up);

        foreach (var position in checkPositions) {
            Cell c = GameField.GetInstance().CellAt(position);
            if (c != null) {
                neighbours.Add(c);
            }
        }
        
        return neighbours;
    }
}