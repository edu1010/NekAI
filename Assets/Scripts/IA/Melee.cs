using System.Collections;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(MovementController2D))]
[RequireComponent(typeof(BasicDamageTaker))]
public class Melee : ExtendedMonoBehaviour
{
    [SerializeField] private float preAttackTime, postAttackTime, stateSwitchTime, damageStunTime;
    [SerializeField] private CollisionDetector2D playerDetector, attackRangeDetector, wallCheck, edgeCheck, groundCheck;
    [SerializeField] private SingleAttackController attackController;

    [SerializeField] [ReadOnly] private State currentState;
    private MovementController2D _movementController;
    private BasicDamageTaker _basicDamageTaker;
    private Transform _playerTransform;
    [SerializeField]
    Animator animator;
    private StateMachine<State> _stateMachine;

    private void Awake()
    {
        animator.enabled = true;
        _movementController = GetComponent<MovementController2D>();
        
        _stateMachine = new StateMachine<State>(State.Idle);

        _stateMachine.OnEnterState(State.Idle, () => InvokeLater(() =>
        {
            if (_stateMachine.CurrentState != State.Idle) return;
            animator.Play("meleWalk");
            _stateMachine.CurrentState = State.Patrol;
        }, stateSwitchTime));

        _stateMachine.OnStayState(State.Idle, () =>
        {
            _movementController.enabled = groundCheck.IsColliding;
            _movementController.StopMoving();
        });

        _stateMachine.OnEnterState(State.Patrol, () => InvokeLater(() =>
        {
            if (_stateMachine.CurrentState != State.Patrol) return;
            animator.Play("meleeIdle"); 
            _stateMachine.CurrentState = State.Idle;
        }, stateSwitchTime));

        _stateMachine.OnStayState(State.Patrol, () =>
        {
            if ((wallCheck.IsColliding || !edgeCheck.IsColliding) && groundCheck.IsColliding) Flip();
            _movementController.enabled = groundCheck.IsColliding;
            _movementController.MoveForward();
        });

        _stateMachine.OnStayState(State.Chase, () =>
        {
            Vector2 direction = _playerTransform.position - transform.position;
            if (direction.x * transform.right.x < 0) Flip();
            _movementController.Move(new Vector2(direction.x, 0));
        });

        _stateMachine.OnEnterState(State.Attack, () => StartCoroutine(AttackRoutine()));
        
        _stateMachine.OnEnterState(State.Damaged, () =>
        {
            _stateMachine.Locked = true;
            _movementController.enabled = false;
            InvokeLater(() =>
            {
                _movementController.enabled = false;
                _stateMachine.Locked = false;
                _stateMachine.CurrentState = _playerTransform is null ? State.Patrol : State.Chase;
            }, damageStunTime);
        });
    }

    public void OnEnable()
    {
        playerDetector.OnCollisionEnter += OnPlayerDetected;
        playerDetector.OnCollisionExit += OnPlayerLost;
        attackRangeDetector.OnCollisionEnter += OnPlayerInRange;
        attackRangeDetector.OnCollisionExit += OnPlayerOutOfRange;
        _basicDamageTaker.OnTakeDamage += OnHit;
    }

    private void OnHit(BasicDamageTaker obj)
    {
        _stateMachine.CurrentState = State.Damaged;
    }

    public void OnDisable()
    {
        playerDetector.OnCollisionEnter -= OnPlayerDetected;
        playerDetector.OnCollisionExit -= OnPlayerLost;
        attackRangeDetector.OnCollisionEnter -= OnPlayerInRange;
        attackRangeDetector.OnCollisionExit -= OnPlayerOutOfRange;
        _basicDamageTaker.OnTakeDamage -= OnHit;
    }

    private void OnPlayerDetected(Collider2D collision)
    {
        _playerTransform = collision.gameObject.transform;
        _stateMachine.CurrentState = State.Chase;
    }

    private void OnPlayerLost(Collider2D collision)
    {
        _playerTransform = null;
        _stateMachine.CurrentState = State.Patrol;
    }

    private void OnPlayerInRange(Collider2D collision)
    {
        _stateMachine.CurrentState = State.Attack;
        animator.Play("meleeAttack");
    }

    private void OnPlayerOutOfRange(Collider2D collision)
    {
        _stateMachine.CurrentState = _playerTransform is null ? State.Patrol : State.Chase;
    }
    
    private void Start()
    {
        _stateMachine.CurrentState = State.Idle;
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
        currentState = _stateMachine.CurrentState;
    }

    private IEnumerator AttackRoutine()
    {
        _stateMachine.Locked = true;
        _movementController.StopMoving();
        yield return new WaitForSeconds(preAttackTime);
        attackController.Attack();
        yield return new WaitForSeconds(postAttackTime);
        _stateMachine.Locked = false;
        _stateMachine.CurrentState = attackRangeDetector.IsColliding ? State.Attack :
            playerDetector.IsColliding ? State.Chase : State.Patrol;
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Damaged
    }
}