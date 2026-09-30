using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PortalBehaviour : MonoBehaviour
{
    [Header("<color=blue>Animation</color>")]
    [SerializeField] private string _statusBoolName = "status";
    
    [Header("<color=blue>Inputs</color>")]
    [SerializeField] private KeyCode _changeColorKey = KeyCode.C;
    [SerializeField] private KeyCode _changeStatusKey = KeyCode.V;
    
    [Header("<color=blue>Rendering</color>")]
    [SerializeField] private Renderer _portalRenderer;
    [SerializeField] private string _bgColorName = "_BackgroundColor";
    [SerializeField] private string _borderColorName = "_BorderColor";
    [SerializeField] private string _starsColorName = "_StarColor";
    [SerializeField] private float _colorChangeInterval = 1.0f;

    private bool _isActive = true, _isCoroutineActive = false;
    
    private Animator _animator;
    private Material _portalMaterial;
    
    private Color _actualBgColor, _newBgColor;
    private Color _actualBorderColor, _newBorderColor;
    private Color _actualStarsColor, _newStarsColor;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _portalMaterial =  _portalRenderer.material;
        
        _actualBgColor = _portalMaterial.GetColor(_bgColorName);
        _actualBorderColor = _portalMaterial.GetColor(_borderColorName);
        _actualStarsColor = _portalMaterial.GetColor(_starsColorName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_changeColorKey) && !_isCoroutineActive && _isActive)
        {
            StartCoroutine(ChangeColor());
        }
        else if (Input.GetKeyDown(_changeStatusKey))
        {
            _isActive = !_isActive;
            _animator.SetBool(_statusBoolName, _isActive);
        }
    }

    private IEnumerator ChangeColor()
    {
        _isCoroutineActive = true;

        float t = 0.0f;
        
        _newBgColor = new Color(Random.Range(0.0f, 0.125f), Random.Range(0.0f, 0.125f), Random.Range(0.0f, 0.125f));
        _newBorderColor = new Color(Random.Range(0.25f, 0.75f), Random.Range(0.25f, 0.75f), Random.Range(0.25f, 0.75f));
        _newStarsColor = new Color(Random.Range(0.75f, 1.0f), Random.Range(0.75f, 1.0f), Random.Range(0.75f, 1.0f));

        while (t < 1.0f)
        {
            t += Time.deltaTime / _colorChangeInterval;
            
            _portalMaterial.SetColor(_bgColorName, Color.Lerp(_actualBgColor, _newBgColor, t));
            _portalMaterial.SetColor(_borderColorName, Color.Lerp(_actualBorderColor, _newBorderColor, t));
            _portalMaterial.SetColor(_starsColorName, Color.Lerp(_actualStarsColor, _newStarsColor, t));

            yield return null;
        }

        _actualBgColor = _newBgColor;
        _actualBorderColor = _newBorderColor;
        _actualStarsColor = _newStarsColor;
        
        _isCoroutineActive = false;
    }
}
