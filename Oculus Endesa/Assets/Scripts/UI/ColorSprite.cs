using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSprite : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color color;

    Color originalColor;

    void Start() {
        originalColor = sprite.color;
    }

    public void ChangeColor() {
        Color _color = new Color(5, 85, 250);
        sprite.color = _color;
    }

    public void OriginalColor() {
        sprite.color = originalColor;
    }
}
