using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : Singleton<CursorManager> {
    
    [SerializeField] private Texture2D _defaultTexture;

    protected override void Awake() => SwitchTexture(_defaultTexture);

    public void SwitchTexture(Texture2D texture) => Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
}