using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(BoxCollider2D))]
public class DragComponent : MonoBehaviour {

    private Block _block;

    private bool _dragging = false;
    private bool _isMouseOver = false;
    
    public void SetBlock(Block block) => _block = block;

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            StartCoroutine(StartDragging());
        }
    }

    private IEnumerator StartDragging() {
        if (_dragging) {
            yield break;
        }

        _dragging = true;
        GameField.GetInstance().LiftBlock(_block);
        
        List<Cell> availableCells = GameField.GetInstance().GetEmptyAdjacentCells(_block);
        availableCells.ForEach(x => x.CellComponent.SetActive(true));

        while (_dragging) {
            FollowCursor();
            if (Input.GetMouseButtonUp(0)) {
                _dragging = false;
            }
            yield return null;
        }

        TryPuttingBlock(availableCells);
    }

    private void TryPuttingBlock(List<Cell> availableCells) {
        CellComponent cellComponent = GetCellUnderBlock();
        Cell closestCell = availableCells[0];
        foreach (Cell c in availableCells) {
            if (c.CellComponent == cellComponent) {
                closestCell = c;
                break;
            }
        }

        GameField.GetInstance().PutBlockAt(_block, closestCell);
        availableCells.ForEach(x => x.CellComponent.SetActive(false));

        if (GameField.GetInstance().CheckWinCondition()) {
            UIManager.GetInstance().ShowWinMenu();
        }
    }

    private CellComponent GetCellUnderBlock() {
        int layerMask = LayerMask.GetMask("Cells");
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100, layerMask);
        if (hit) {
            return hit.transform.GetComponent<CellComponent>();
        }
        return null;
    }

    private void FollowCursor() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -2);
    }
}