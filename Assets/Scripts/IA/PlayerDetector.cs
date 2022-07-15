using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector2D))]
public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private LayerMask walls;
    
    private CollisionDetector2D _playerDetector;
    private bool _playerDetected;
    private Transform _playerTransform;

    public event Action<Transform> OnPlayerDetected;
    public event Action OnPlayerLost; 

    private void Awake()
    {
        _playerDetector = GetComponent<CollisionDetector2D>();
        _playerDetected = false;
    }

    private void OnEnable()
    {
        _playerDetector.OnCollisionEnter += PlayerDetected;
        _playerDetector.OnCollisionExit += PlayerLost;
    }
    
    private void OnDisable()
    {
        _playerDetector.OnCollisionEnter -= PlayerDetected;
        _playerDetector.OnCollisionExit -= PlayerLost;
    }

    private void PlayerLost(Collider2D obj)
    {
        _playerTransform = null;
        if (!_playerDetected) return;
        OnPlayerLost?.Invoke();
        _playerDetected = false;
    }

    private void PlayerDetected(Collider2D obj)
    {
        _playerTransform = obj.gameObject.transform;
    }

    private void Update()
    {
        if (_playerTransform is null) return;
        var position = transform.position;
        var playerPosition = _playerTransform.position;
        var distanceToPlayer =
            Mathf.Sqrt(Mathf.Pow(playerPosition.x - position.x, 2) + Mathf.Pow(playerPosition.y - position.y, 2));
        var directionToPlayer = playerPosition - position;
        var hit = Physics2D.Raycast(position, directionToPlayer, distanceToPlayer, walls);
        switch (_playerDetected)
        {
            case true when hit.collider is { }:
                OnPlayerLost?.Invoke();
                _playerDetected = false;
                break;
            case false when hit.collider is null:
                OnPlayerDetected?.Invoke(_playerTransform);
                _playerDetected = true;
                break;
        }
    }
}
