using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CellComponent : MonoBehaviour {
    
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _glowingMaterial;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetActive(false);
    }

    public void SetActive(bool active) => _spriteRenderer.material = (active) ? _glowingMaterial : _defaultMaterial;
}