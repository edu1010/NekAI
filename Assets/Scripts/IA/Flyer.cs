using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(BasicDamageTaker))]
[RequireComponent(typeof(MovementController2D))]
public class Flyer : ExtendedMonoBehaviour
{
    [SerializeField] private float stunnedTime = 0.1f;
    [SerializeField] private PlayerDetector playerDetector;
    [SerializeField] [ReadOnly] private Transform _playerTransform;
    private BasicDamageTaker _basicDamageTaker;
    private MovementController2D _movementController;

    private StateMachine<State> _stateMachine;

    private void Awake()
    {
        _movementController = GetComponent<MovementController2D>();
        _basicDamageTaker = GetComponent<BasicDamageTaker>();
        InitFsm();
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
    }

    private void OnEnable()
    {
        playerDetector.OnPlayerDetected += OnPlayerDetected;
        playerDetector.OnPlayerLost += OnPlayerLost;
        _basicDamageTaker.OnTakeDamage += OnHit;
    }

    private void OnDisable()
    {
        playerDetector.OnPlayerDetected -= OnPlayerDetected;
        playerDetector.OnPlayerLost -= OnPlayerLost;
        _basicDamageTaker.OnTakeDamage -= OnHit;
    }

    private void OnHit(BasicDamageTaker obj)
    {
        _stateMachine.CurrentState = State.Damaged;
    }

    private void OnPlayerLost()
    {
        _playerTransform = null;
        _stateMachine.CurrentState = State.Idle;
    }

    private void OnPlayerDetected(Transform t)
    {
        _playerTransform = t;
        _stateMachine.CurrentState = State.Chase;
    }

    private void InitFsm()
    {
        _stateMachine = new StateMachine<State>(State.Idle);
        _stateMachine.OnEnterState(State.Idle, () => _movementController.StopMoving());
        _stateMachine.OnStayState(State.Chase, () =>
        {
            var direction = _playerTransform.position - transform.position;
            if (direction.x * transform.right.x < 0) transform.Rotate(0f, 180f, 0f);
            _movementController.Move(direction);
        });
        _stateMachine.OnEnterState(State.Damaged, () =>
        {
            _stateMachine.Locked = true;
            _movementController.enabled = false;
            InvokeLater(() =>
            {
                _stateMachine.Locked = false;
                _stateMachine.CurrentState = _playerTransform is null ? State.Idle : State.Chase;
            }, stunnedTime);
        });
        _stateMachine.OnExitState(State.Damaged, () => { _movementController.enabled = true; });
        _stateMachine.CurrentState = State.Idle;
    }

    private enum State
    {
        Idle,
        Chase,
        Damaged
    }
}