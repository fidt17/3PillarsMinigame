using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DragComponent : MonoBehaviour {

    private Block _block;

    private bool _dragging = false;
    private bool _isMouseOver = false;
    
    public void SetBlock(Block block) => _block = block;

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            StartCoroutine(StartDragging());
        } else if (Input.GetMouseButtonUp(0) && _dragging) {
            _dragging = false;
        }
    }

    private IEnumerator StartDragging() {
        if (_dragging) {
            yield break;
        }

        _dragging = true;
        GameField.GetInstance().LiftBlock(_block);
        
        List<CellComponent> availableCells = GameField.GetInstance().GetEmptyAdjacentCells(_block);
        availableCells.ForEach(x => x.SetActive(true));

        while (_dragging) {
            FollowCursor();
            yield return null;
        }

        availableCells.ForEach(x => x.SetActive(false));
        TryPuttingBlock(availableCells);
    }

    private void TryPuttingBlock(List<CellComponent> availableCells) {
        CellComponent cell = GetCellUnderBlock();
        if (!availableCells.Contains(cell)) {
            cell = null;
        }

        Vector2Int newPosition = (cell != null) ? new Vector2Int((int) cell.transform.position.x, (int) cell.transform.position.y) : _block.Position;
        _block.SetPosition(newPosition);

        GameField.GetInstance().PutBlock(_block);
        GameField.GetInstance().CheckWinCondition();
    }

    private CellComponent GetCellUnderBlock() {
        int layerMask = LayerMask.GetMask("Cells");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, layerMask);
        if (hit) {
            CellComponent cell = hit.transform.GetComponent<CellComponent>();
            return cell;
        }
        return null;
    }

    private void FollowCursor() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -2);
    }
}