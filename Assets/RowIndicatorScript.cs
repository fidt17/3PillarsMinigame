using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowIndicatorScript : MonoBehaviour {

    [SerializeField] private Sprite[] _circleSprites = new Sprite[6];
    
    private SpriteRenderer sr;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(int blockCount) {
        sr.sprite = _circleSprites[blockCount];
    }
}