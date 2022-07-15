using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpController2D : MonoBehaviour
{
    [SerializeField] private float minHeight = 2;
    [SerializeField] private float maxHeight = 3;
    [SerializeField] private float apexTime = 0.4f;
    [SerializeField] private float maxVerticalSpeed = 100f;
    [SerializeField] private CollisionDetector2D groundCheck, jumpCheck;

    [SerializeField] [ReadOnly] private float gravity, maxVelocityY, minVelocityY, currentVelocityY;
    [SerializeField] [ReadOnly] private bool isJumping;

    private Rigidbody2D _rigidbody;

    public CollisionDetector2D GroundCheck => groundCheck;

    public CollisionDetector2D JumpCheck => jumpCheck;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        gravity = -(2 * maxHeight) / (apexTime * apexTime);
        maxVelocityY = Mathf.Abs(gravity) * apexTime;
        minVelocityY = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minHeight);
    }

    private void FixedUpdate()
    {
        var velocity = _rigidbody.velocity;
        currentVelocityY = isJumping switch
        {
            true when GroundCheck.IsColliding => maxVelocityY,
            false when currentVelocityY > minVelocityY => minVelocityY,
            _ => JumpCheck.IsColliding ? 0 : velocity.y + gravity * Time.fixedDeltaTime
        };
        velocity = new Vector2(velocity.x, Mathf.Clamp(currentVelocityY, -maxVerticalSpeed, maxVerticalSpeed));
        _rigidbody.velocity = velocity;
    }

    private void OnEnable()
    {
        groundCheck.OnCollisionEnter += StopJumping;
    }

    private void OnDisable()
    {
        groundCheck.OnCollisionEnter -= StopJumping;
    }

    private void StopJumping(Collider2D obj)
    {
        StopJumping();
    }

    public event Action OnStartJumping, OnStopJumping;

    public void StartJumping()
    {
        isJumping = true;
        OnStartJumping?.Invoke();
    }

    public void StopJumping()
    {
        isJumping = false;
        OnStopJumping?.Invoke();
    }
}