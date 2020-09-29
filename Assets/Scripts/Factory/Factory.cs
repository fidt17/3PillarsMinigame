using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Singleton<Factory> {

    [Header("Cells")]
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _cellParent;

    [Header("Blocks")]
    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private Transform _blockParent;

    [Header("Colors")]
    [SerializeField] private List<Color> _colorVariants;
    [SerializeField] private Color _solidBlockColor;

    public GameObject CreateCell(int x, int y) {
        GameObject cell = Instantiate(_cellPrefab, _cellParent);
        cell.transform.position = new Vector3(x, y, 0);
        return cell;
    }

    public GameObject CreateBlockOfType(BlockType type) {
        GameObject obj = Instantiate(_blockPrefab, _blockParent);
        obj.GetComponent<SpriteRenderer>().color = GetBlockColorByType(type);
        return obj;
    }

    private Color GetBlockColorByType(BlockType type) => (type == BlockType.solid) ? _solidBlockColor : _colorVariants[(int) type];
}