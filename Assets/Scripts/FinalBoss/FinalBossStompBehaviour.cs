using UnityEngine;

[RequireComponent(typeof(MovementController2D))]
[RequireComponent(typeof(FinalBoss))]
public class FinalBossStompBehaviour : ExtendedMonoBehaviour
{
    public enum State
    {
        WalkToPlayer,
        Stomp,
        Disabled
    }

    [SerializeField] private float distanceToStomp, stompDuration;
    [SerializeField] [ReadOnly] private int repetitions;

    [SerializeField] [ReadOnly] private State currentState;

    private Animator _animator;
    private FinalBoss _finalBoss;
    private MovementController2D _movementController2D;
    private StompEffect _stompEffect;
    private Transform _target;

    public StateMachine<State> Machine { get; private set; }

    private void Awake()
    {
        _stompEffect = GetComponentInChildren<StompEffect>();
        _finalBoss = GetComponent<FinalBoss>();
        _animator = GetComponentInChildren<Animator>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _movementController2D = GetComponent<MovementController2D>();

        Machine = new StateMachine<State>(State.Disabled);

        InitWalkToPlayerState();
        InitStompState();
    }

    private void Update()
    {
        currentState = Machine.CurrentState;
    }

    private void OnEnable()
    {
        repetitions = Random.Range(1, 5);
        Machine.CurrentState = State.WalkToPlayer;
    }

    private void InitStompState()
    {
        Machine.OnEnterState(State.Stomp, () =>
        {
            _animator.Play("stampAttackRobot");
            _movementController2D.StopMoving();
            repetitions--;
            InvokeLater(() =>
            {
                if (repetitions == 0) _finalBoss.ChangeToRandomState();
                else Machine.CurrentState = State.WalkToPlayer;
            }, stompDuration);
            
            InvokeLater(() => _stompEffect.DoStompEffect(), 0.5f);
        });
    }

    private void InitWalkToPlayerState()
    {
        Machine.OnEnterState(State.WalkToPlayer, () => _animator.Play("walkRobot"));

        Machine.OnStayState(State.WalkToPlayer, () =>
        {
            var distance = _target.position - transform.position;
            _movementController2D.Move(Vector2.right * distance);
            if (distance.x * transform.right.x < 0) transform.Rotate(0f, 180f, 0f);
            if (!(Mathf.Abs(distance.x) > distanceToStomp)) Machine.CurrentState = State.Stomp;
        });
    }
}