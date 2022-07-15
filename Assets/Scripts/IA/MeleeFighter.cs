using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(MovementController2D))]
public class MeleeFighter : ExtendedMonoBehaviour
{
    [SerializeField] private float preAttackTime, postAttackTime, stateSwitchTime;
    [SerializeField] private CollisionDetector2D playerDetector, attackRangeDetector, wallCheck, edgeCheck, groundCheck;
    [SerializeField] private SingleAttackController attackController;

    [SerializeField] [ReadOnly] private State currentState;

    private FSM<State> _fsm;
    private MovementController2D _movementController;
    private Transform _playerTransform;

    public void Awake()
    {
        _movementController = GetComponent<MovementController2D>();
        InitFSM();
    }

    public void FixedUpdate()
    {
        _fsm.Update();
        currentState = _fsm.GetState();
    }

    public void OnEnable()
    {
        playerDetector.OnCollisionEnter += OnPlayerDetected;
        playerDetector.OnCollisionExit += OnPlayerLost;
        attackRangeDetector.OnCollisionEnter += OnPlayerInRange;
        attackRangeDetector.OnCollisionExit += OnPlayerOutOfRange;
    }

    public void OnDisable()
    {
        playerDetector.OnCollisionEnter -= OnPlayerDetected;
        playerDetector.OnCollisionExit -= OnPlayerLost;
        attackRangeDetector.OnCollisionEnter -= OnPlayerInRange;
        attackRangeDetector.OnCollisionExit -= OnPlayerOutOfRange;
    }

    private void OnPlayerDetected(Collider2D collision)
    {
        _playerTransform = collision.gameObject.transform;
        _fsm.SetState(State.Chase);
    }

    private void OnPlayerLost(Collider2D collision)
    {
        _playerTransform = null;
        _fsm.SetState(State.Patrol);
    }

    private void OnPlayerInRange(Collider2D collision)
    {
        _fsm.SetState(State.Attack);
    }

    private void OnPlayerOutOfRange(Collider2D collision)
    {
        _fsm.SetState(_playerTransform == null ? State.Patrol : State.Chase);
    }

    private void InitFSM()
    {
        _fsm = new FSM<State>(State.Idle);
        
        _fsm.OnEnterState(State.Idle, () =>
        {
            InvokeLater(() =>
            {
                if (_fsm.GetState() == State.Idle) _fsm.SetState(State.Patrol);
            }, stateSwitchTime);
        });

        _fsm.OnStayState(State.Idle, () => { _movementController.StopMoving(); });

        _fsm.OnEnterState(State.Patrol, () =>
        {
            InvokeLater(() =>
            {
                if (_fsm.GetState() == State.Patrol) _fsm.SetState(State.Idle);
            }, stateSwitchTime);
        });

        _fsm.OnStayState(State.Patrol, () =>
        {
            if ((wallCheck.IsColliding || !edgeCheck.IsColliding) && groundCheck.IsColliding) Flip();
            _movementController.MoveForward();
            _movementController.enabled = groundCheck.IsColliding;
        });

        _fsm.OnStayState(State.Chase, () =>
        {
            Vector2 direction = _playerTransform.position - transform.position;
            if (direction.x * transform.right.x < 0) Flip();
            if (direction.x > 0) _movementController.MoveRight();
            if (direction.x < 0) _movementController.MoveLeft();
            if (direction.x == 0) _movementController.StopMoving();
            _movementController.enabled = groundCheck.IsColliding;
        });

        _fsm.OnEnterState(State.Attack, () => { StartCoroutine(AttackRoutine()); });
        
        _fsm.OnStayState(State.Patrol, () =>
        {
            _movementController.enabled = groundCheck.IsColliding;
        });
    }

    private IEnumerator AttackRoutine()
    {
        _movementController.StopMoving();
        _fsm.LockState();
        yield return new WaitForSeconds(preAttackTime);
        if (_fsm.GetState() == State.Attack) attackController.Attack();
        yield return new WaitForSeconds(postAttackTime);
        _fsm.UnlockState();
        if (attackRangeDetector.IsColliding) _fsm.SetState(State.Attack);
        else if (playerDetector.IsColliding) _fsm.SetState(State.Chase);
        else _fsm.SetState(State.Patrol);
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