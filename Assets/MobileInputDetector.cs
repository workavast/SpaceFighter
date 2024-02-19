using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInputDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Camera _camera;
    private PointerEventData _pointerEventData;

    public Vector2 Position => _playerStartPosition + (_currentPosition - _startPosition);
    private Vector2 _currentPosition;
    private Vector2 _startPosition;

    private Transform _playerTransform;
    private Vector2 _playerStartPosition;
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_pointerEventData != null)
            _currentPosition = _camera.ScreenToWorldPoint(_pointerEventData.position);
    }

    public void SetPlayerTransform(Transform playerTransform) => _playerTransform = playerTransform;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_playerTransform == null)
            throw new NullReferenceException();
        
        _pointerEventData = eventData;
        _startPosition =_camera.ScreenToWorldPoint(_pointerEventData.position);
        _playerStartPosition = _playerTransform.position;
    }

    public void OnPointerUp(PointerEventData eventData) => _pointerEventData = null;
}
