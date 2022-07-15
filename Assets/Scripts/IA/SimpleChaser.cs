using UnityEngine;

[RequireComponent(typeof(MovementController2D))]
[RequireComponent(typeof(BasicDamageTaker))]
public class SimpleChaser : ExtendedMonoBehaviour
{
    [SerializeField] private float damagedStopTime = 0.2f;
    [SerializeField] private CollisionDetector2D playerDetector;
    [SerializeField] private CollisionDetector2D groundCheck;
    private BasicDamageTaker _basicDamageTaker;
    private StateMachine<State> fsm;

    private MovementController2D movementController;
    private Transform playerTransform;

    public void Awake()
    {
        movementController = GetComponent<MovementController2D>();
        _basicDamageTaker = GetComponent<BasicDamageTaker>();
        InitFsm();
    }

    public void FixedUpdate()
    {
        movementController.enabled = groundCheck.IsColliding;
        fsm.Update();
    }

    public void OnEnable()
    {
        playerDetector.OnCollisionEnter += OnPlayerDetected;
        playerDetector.OnCollisionExit += OnPlayerLost;
        _basicDamageTaker.OnTakeDamage += OnHit;
    }

    public void OnDisable()
    {
        playerDetector.OnCollisionEnter -= OnPlayerDetected;
        playerDetector.OnCollisionExit -= OnPlayerLost;
        _basicDamageTaker.OnTakeDamage -= OnHit;
    }

    private void OnHit(BasicDamageTaker obj)
    {
        fsm.CurrentState = State.Damaged;
    }

    private void InitFsm()
    {
        fsm = new StateMachine<State>(State.Idle);

        fsm.OnStayState(State.Idle, () => { movementController.StopMoving(); });

        fsm.OnStayState(State.Chase, () =>
        {
            Vector2 direction = playerTransform.position - transform.position;
            if (direction.x * transform.right.x < 0) Flip();
            if (direction.x > 0) movementController.MoveRight();
            if (direction.x < 0) movementController.MoveLeft();
            if (direction.x == 0) movementController.StopMoving();
        });

        fsm.OnEnterState(State.Damaged, () =>
        {
            movementController.enabled = false;
            InvokeLater(() =>
            {
                movementController.enabled = true;
                fsm.CurrentState = playerTransform is null ? State.Idle : State.Chase;
            }, damagedStopTime);
        });
    }

    private void OnPlayerDetected(Collider2D collision)
    {
        playerTransform = collision.gameObject.transform;
        fsm.CurrentState = State.Chase;
    }

    private void OnPlayerLost(Collider2D collision)
    {
        playerTransform = null;
        fsm.CurrentState = State.Idle;
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private enum State
    {
        Chase,
        Idle,
        Damaged
    }
}