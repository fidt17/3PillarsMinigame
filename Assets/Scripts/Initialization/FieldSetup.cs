using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSetup : Singleton<FieldSetup> {

    [Header("Cells")]
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _cellParent;

    [Header("Blocks")]
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private Transform _blockParent;

    [Header("Colors")]
    [SerializeField] private List<Color> _colorVariants;
    [SerializeField] private Color _solidBlockColor;

    public void StartNewGame() {
        CreateCells();
        InitializeRowIndicators();
        CreateBlocks();        
    }

    public void RestartGame() {
        GameField.GetInstance().Data.DeleteBlocks();
        CreateBlocks();
    }

    private void CreateCells() {
        for (int x = 0; x < 5; x++) {
            for (int y = 0; y < 5; y++) {
                GameObject cell = Instantiate(_cellPrefab, _cellParent);
                cell.transform.position = new Vector3(x, y, 0);
                GameField.GetInstance().AddCellAt(new Vector2Int(x, y), cell);
            }
        }
    }

    private void InitializeRowIndicators() {
        GameField gameField = GameField.GetInstance();
        for (int i = 0; i < _colorVariants.Count; i++) {
            GameField.GetInstance().ChangeRowColorAt(i, _colorVariants[i]);
        }
    }

    private void CreateBlocks() {
        //Solid blocks
        for (int x = 1; x <= 3; x += 2) {
            for (int y = 0; y <= 4; y += 2) {
                GameObject blockObject = Instantiate(_blockPrefab, _blockParent);
                blockObject.GetComponent<SpriteRenderer>().color = _solidBlockColor;
                Block block = new Block(new Vector2Int(x, y), BlockType.solid, blockObject);
                GameField.GetInstance().Data.AddBlock(block);
            }
        }

        //Draggable blocks
        for (int colorIndex = 0; colorIndex < 3; colorIndex++) {
            for (int i = 0; i < 5; i++) {
                
                Vector2Int randomPosition;
                while (true) {
                    randomPosition = new Vector2Int((int) Random.Range(0, 5), (int) Random.Range(0, 5));
                    if (GameField.GetInstance().Data.BlockAt(randomPosition) == null) {
                        break;
                    }
                }

                GameObject blockObject = Instantiate(_blockPrefab, _blockParent);
                blockObject.GetComponent<SpriteRenderer>().color = _colorVariants[colorIndex];
                Block block = new Block(randomPosition, GetBlockTypeByIndex(colorIndex), blockObject);
                GameField.GetInstance().Data.AddBlock(block);
            }
        }
        
        /*
        for (int x = 0; x < 5; x += 2) {
            for (int y = 0; y < 5; y++) {
                GameObject blockObject = Instantiate(_blockPrefab, _blockParent);
                blockObject.GetComponent<SpriteRenderer>().color = _colorVariants[x/2];
                Vector2Int position = new Vector2Int(x, y);
                Block block = new Block(position, GetBlockTypeByPosition(position), blockObject);
                GameField.GetInstance().Data.AddBlock(block);
            }
        }
        */
    }

    private BlockType GetBlockTypeByIndex(int index) {
        switch(index) {
            case 0:
                return BlockType.type0;
            
            case 1:
                return BlockType.type1;

            case 2:
                return BlockType.type2;
            
            default:
                return BlockType.solid;
        }
    }
}