using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    private Color _defaultColor = new Color(255, 255, 255);
    private Color _redColor = new Color(255, 50, 50);
    private Color _greenColor = new Color(50, 255, 50);
    private Color _blueColor = new Color(50, 50, 255);
    private Color _yellowColor = new Color(255, 255, 50);

    private Color _currentColor = new Color(255, 255, 255);
    private ParticleSystem _particleSystem;

    private void Start() 
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        SetParticleColor();
        
    }

    private void SetParticleColor()
    {
        var _pm = _particleSystem.main;
        _pm.startColor = new ParticleSystem.MinMaxGradient(_yellowColor);
        Debug.Log(_pm.startColor.color);
    }

    public void SetColor(string color)
    {
        switch (color)
        {
            case "Default": _currentColor = _defaultColor;
            break;

            case "Red": _currentColor = _redColor;
            break;

            case "Green": _currentColor = _greenColor;
            break;

            case "Blue": _currentColor = _blueColor;
            break;

            case "Yellow": _currentColor = _yellowColor;
            break;

            default: _currentColor = _defaultColor;
            break;
        }
    }
}
