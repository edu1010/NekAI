using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlicker : ExtendedMonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flickerTime, transitionTime;
    [SerializeField] private Color targetColor;
    
    private Color _defaultColor, _currentColor;

    private bool _isFlickering;
    private bool _isOnTargetColor;

    private void Awake()
    {
        _isFlickering = false;
        _isOnTargetColor = false;
        _defaultColor = spriteRenderer.material.color;
        _currentColor = _defaultColor;
    }

    public void StartFlicker()
    {
        _isFlickering = true;
        Flicker();
    }

    public void StopFLicker()
    {
        _isFlickering = false;
    }

    private void Flicker()
    {
        if (!_isFlickering)
        {
            _currentColor = _defaultColor;
            return;
        }
        InvokeLater(() =>
        {
            _currentColor = _isOnTargetColor ? _defaultColor : targetColor;
            _isOnTargetColor = !_isOnTargetColor;
            Flicker();
        }, flickerTime);
    }

    private void FixedUpdate()
    {
        if (spriteRenderer.material.color == _currentColor) return;
        spriteRenderer.material.color = Color.Lerp(spriteRenderer.material.color, _currentColor, Time.deltaTime / transitionTime);
    }
}
