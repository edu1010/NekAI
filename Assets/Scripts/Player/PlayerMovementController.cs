using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DashChecker))]
public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Parameters")] [Tooltip("Movement speed in units per second")] [SerializeField]
    private float movementSpeed = 10f;

    [Tooltip("Seconds to reach max speed")] [SerializeField]
    private float smoothingTime = 0.1f;

    [SerializeField] private float wallSlideSpeed = 3f;

    [SerializeField] private CollisionDetector2D wallCheck;

    [SerializeField] [ReadOnly] private float targetVelocityX, currentVelocityX, velocityX;

    [Header("Jump Parameters")] [SerializeField]
    private float minHeight = 2;

    [SerializeField] private float maxHeight = 3;
    [SerializeField] private float apexTime = 0.4f;
    [SerializeField] private CollisionDetector2D groundDetector;

    [SerializeField] [ReadOnly] private float gravity, maxVelocityY, minVelocityY, currentVelocityY;
    [SerializeField] [ReadOnly] private bool isFlying, isJumping, isShooting;

    [Header("Dash Parameters")] [SerializeField]
    private float dashDistance = 4;

    [SerializeField] private float dashDelayTime = 0.2f;
    [SerializeField] private float dashReloadTime = 0.5f;

    [SerializeField] [ReadOnly] private bool isDashing;
    [SerializeField] [ReadOnly] private float dashDelayTimer, dashReloadTimer, currentDashDistance;
    private DashChecker _dashChecker;

    private Rigidbody2D _rigidbody;

    public bool IsShooting
    {
        get => isShooting;
        set => isShooting = value;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _dashChecker = GetComponent<DashChecker>();
        gravity = -(2 * maxHeight) / Mathf.Pow(apexTime, 2);
        maxVelocityY = Mathf.Abs(gravity) * apexTime;
        minVelocityY = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minHeight);
        currentDashDistance = dashDistance;
    }

    public void FixedUpdate()
    {
        currentVelocityX = Mathf.SmoothDamp(_rigidbody.velocity.x, targetVelocityX, ref velocityX, smoothingTime);

        currentVelocityY += gravity * Time.fixedDeltaTime;

        dashReloadTimer += Time.fixedDeltaTime;

        if (isDashing)
        {
            dashDelayTimer += Time.fixedDeltaTime;
            currentVelocityY = 0;
            currentVelocityX = 0;
            if (dashDelayTimer >= dashDelayTime)
            {
                var t = transform;
                t.position = t.position + t.right * currentDashDistance;
                isDashing = false;
                dashReloadTimer = 0;
            }
        }
        else
        {
            dashDelayTimer = 0;
        }

        if (currentVelocityY < 0 && wallCheck.IsColliding || IsShooting) currentVelocityY = -wallSlideSpeed;

        _rigidbody.velocity = new Vector2(currentVelocityX, currentVelocityY);

        if (groundDetector.IsColliding) currentVelocityY = 0;

        if (isFlying || isJumping && groundDetector.IsColliding) currentVelocityY = maxVelocityY;

        if (!isFlying && !isJumping && currentVelocityY > minVelocityY) currentVelocityY = minVelocityY;
    }

    public void Move(float direction)
    {
        targetVelocityX = direction * movementSpeed;
    }

    public void FlyStart()
    {
        isFlying = true;
    }

    public void FlyStop()
    {
        isFlying = false;
    }

    public void JumpStart()
    {
        isJumping = true;
    }

    public void JumpStop()
    {
        isJumping = false;
    }

    public void Dash()
    {
        currentDashDistance = _dashChecker.GetDashDistance(dashDistance);
        if (dashReloadTimer >= dashReloadTime) isDashing = true;
    }
}