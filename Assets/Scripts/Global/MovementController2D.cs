using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController2D : MonoBehaviour
{
    [Tooltip("Movement speed in units per second")] [SerializeField]
    private float movementSpeed = 10f;

    [Tooltip("Seconds to reach movement speed")] [SerializeField]
    private float smoothingTime = 0.1f;

    [SerializeField] [ReadOnly] private Vector2 currentVelocity;

    [SerializeField] [ReadOnly] private Vector2 targetVelocity;

    [SerializeField] [ReadOnly] private bool isMoving;

    private Rigidbody2D _rigidbody;

    public bool IsMoving
    {
        get => isMoving;
        private set => isMoving = value;
    }

    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    public float SmoothingTime
    {
        get => smoothingTime;
        set => smoothingTime = value;
    }

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        isMoving = false;
    }

    public void FixedUpdate()
    {
        var velocity = _rigidbody.velocity;
        _rigidbody.velocity = Vector2.SmoothDamp(velocity,
            targetVelocity.y == 0 ? new Vector2(targetVelocity.x, velocity.y) : targetVelocity, ref currentVelocity,
            smoothingTime);
        isMoving = targetVelocity != Vector2.zero;
    }

    public void Move(Vector2 direction)
    {
        direction.Normalize();
        targetVelocity = direction * movementSpeed;
    }

    public void MoveRight()
    {
        targetVelocity = Vector2.right * movementSpeed;
    }

    public void MoveLeft()
    {
        targetVelocity = Vector2.left * movementSpeed;
    }

    public void MoveUp()
    {
        targetVelocity = Vector2.up * movementSpeed;
    }

    public void MoveDown()
    {
        targetVelocity = Vector2.down * movementSpeed;
    }

    public void StopMoving()
    {
        targetVelocity = Vector2.zero;
    }

    public void MoveForward()
    {
        targetVelocity = transform.right * movementSpeed;
    }
}