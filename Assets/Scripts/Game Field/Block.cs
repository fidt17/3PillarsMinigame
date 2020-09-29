using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType {
    solid = -1,
    type0 =  0,
    type1 =  1,
    type2 =  2
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

    public Block(Vector2Int position, BlockType type, GameObject obj) {
        _type = type;
        _gameObject = obj;
        
        SetPosition(position);
        if (Type != BlockType.solid)  {
            AddDragComponent();
        }
    }

    public void SetPosition(Vector2Int position) {
        _position = position;
        _gameObject.transform.position = new Vector3(position.x, position.y, -1);
    }

    public void Destroy() => GameObject.Destroy(_gameObject);

    private void AddDragComponent() {
        _gameObject.AddComponent<DragComponent>();
        _gameObject.GetComponent<DragComponent>().SetBlock(this);
    }
}