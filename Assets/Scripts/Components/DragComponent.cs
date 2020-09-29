using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DragComponent : MonoBehaviour {

    private Block _block;

    private bool _dragging = false;
    private bool _isMouseOver = false;
    
    private List<CellComponent> _possibleCells;

    public void SetBlock(Block block) => _block = block;

    private void Update() {
        if (_dragging) {
            FollowCursor();
        }
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            _dragging = true;
            GameField.GetInstance().Data.RemoveBlock(_block);
            _possibleCells = GameField.GetInstance().GetPossibleCellsForBlock(_block);
            _possibleCells.ForEach(x => x.SetActive(true));
        } else if (Input.GetMouseButtonUp(0) && _dragging) {
            _dragging = false;
            _possibleCells.ForEach(x => x.SetActive(false));
            TryPuttingBlock();
        }
    }

    private void TryPuttingBlock() {
        CellComponent closestCell = GetCellUnderBlock();
        Vector2Int newPosition = (closestCell != null) ? new Vector2Int((int) closestCell.transform.position.x, (int) closestCell.transform.position.y) : _block.Position;
        _block.SetPosition(newPosition);

        GameField.GetInstance().Data.AddBlock(_block);
        GameField.GetInstance().CheckWinCondition();
    }

    private CellComponent GetCellUnderBlock() {
        int layerMask = LayerMask.GetMask("Cells");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, layerMask);
        if (hit) {
            CellComponent cell = hit.transform.GetComponent<CellComponent>();
            return (_possibleCells.Contains(cell)) ? cell : null;
        }
        return null;
    }

    private void FollowCursor() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -2);
    }
}