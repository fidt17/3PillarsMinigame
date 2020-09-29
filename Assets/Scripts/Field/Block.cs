using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType {
    solid,
    type0,
    type1,
    type2
}

public class Block {

    public Vector2Int Position => _position;
    public int X => _position.x;
    public int Y => _position.y;

    public BlockType Type => _type;
    public GameObject GameObject => _blockObject;

    private Vector2Int _position;
    private BlockType _type;
    private GameObject _blockObject;

    public Block(Vector2Int position, BlockType type, GameObject blockObject) {
        _type = type;
        _blockObject = blockObject;
        
        SetPosition(position);
        SetUpGameObject();
    }

    public void SetPosition(Vector2Int position) {
        _position = position;
        _blockObject.transform.position = new Vector3(position.x, position.y, -1);
    }

    public void Destroy() => GameObject.Destroy(_blockObject);

    private void SetUpGameObject() {
        if (Type != BlockType.solid) {
            _blockObject.AddComponent<DragComponent>();
            _blockObject.GetComponent<DragComponent>().SetBlock(this);
        }
    }
}