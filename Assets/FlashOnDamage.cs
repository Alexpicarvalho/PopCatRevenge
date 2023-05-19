using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnDamage : MonoBehaviour
{
    public float _flashTime;
    public Color _flashColor;  

    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;
    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;

    }
    public void Flash()
    {
        _spriteRenderer.color = _flashColor;
        Invoke("UnFlash", _flashTime);
    }

    public void UnFlash()
    {
        _spriteRenderer.color = _defaultColor;
    }
}
