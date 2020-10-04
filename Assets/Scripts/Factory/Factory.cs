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
    [SerializeField] private List<Sprite> _blockVariants;

    public GameObject CreateCell(int x, int y) => Instantiate(_cellPrefab, new Vector3(x, y, 0), Quaternion.identity, _cellParent);

    public GameObject CreateBlockOfType(BlockType type) {
        GameObject obj = Instantiate(_blockPrefab, _blockParent);
        obj.GetComponent<SpriteRenderer>().sprite = _blockVariants[(int) type];
        return obj;
    }
}