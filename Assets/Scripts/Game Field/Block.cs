using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType {
    type0,
    type1,
    type2,
    solid
}

public class Block {

    public Vector2Int Position => _position;
    public int X => _position.x;
    public int Y => _position.y;

    public BlockType Type => _type;
    public GameObject GameObject => _gameObject;

    private Vector2Int _position;
    private BlockType _type;
    private GameObject _gameObject;

    public Block(BlockType type, GameObject obj) {
        _type = type;
        _gameObject = obj;
        
        if (_type != BlockType.solid)  {
            AddDragComponent();
        }
    }

    public void SetPosition(Vector2Int position) {
        _position = position;
        _gameObject.transform.position = new Vector3(_position.x, _position.y, -1);
    }

    private void AddDragComponent() {
        _gameObject.AddComponent<DragComponent>();
        _gameObject.GetComponent<DragComponent>().SetBlock(this);
    }
}