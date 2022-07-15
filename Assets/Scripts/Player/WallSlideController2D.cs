using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WallSlideController2D : MonoBehaviour
{
    [SerializeField] private float slideSpeed;
    [SerializeField] private CollisionDetector2D wallCheck, groundCheck;
    [SerializeField] private RaycastDetector2D wallDetector;

    [SerializeField] [ReadOnly] private bool isWallSliding;

    private float _previousVelocityY;

    private Rigidbody2D _rigidbody;

    public bool IsWallSliding => isWallSliding;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _previousVelocityY = 0f;
    }

    private void FixedUpdate()
    {
        if (wallCheck.IsColliding && !groundCheck.IsColliding && _previousVelocityY > 0 &&
            _rigidbody.velocity.y < 0) OnStart();
        if (isWallSliding && !wallCheck.IsColliding) OnStop();

        if (isWallSliding)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -slideSpeed);
            if (IsFacingWall()) transform.Rotate(0f, 180f, 0f);
        }

        _previousVelocityY = _rigidbody.velocity.y;
    }

    private void OnEnable()
    {
        wallCheck.OnCollisionEnter += OnWallCheckCollisionEnter;
    }

    private void OnDisable()
    {
        wallCheck.OnCollisionEnter -= OnWallCheckCollisionEnter;
    }

    private bool IsFacingWall()
    {
        return wallDetector.IsRaycastCollision();
    }

    private void OnWallCheckCollisionEnter(Collider2D collision)
    {
        if (_rigidbody.velocity.y < 0 && !groundCheck.IsColliding) OnStart();
    }

    public event Action OnStartWallSliding, OnStopWallSliding;

    private void OnStop()
    {
        isWallSliding = false;
        OnStopWallSliding?.Invoke();
    }

    private void OnStart()
    {
        isWallSliding = true;
        OnStartWallSliding?.Invoke();
    }

    public void StartWallSliding()
    {
        isWallSliding = true;
    }

    public void StopWallSliding()
    {
        isWallSliding = false;
    }
}