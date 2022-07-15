using UnityEngine;

[RequireComponent(typeof(MovementController2D))]
[RequireComponent(typeof(FinalBoss))]
public class FinalBossLaserBehaviour : ExtendedMonoBehaviour
{
    public enum State
    {
        WalkToTarget,
        Aim,
        Charge,
        Fire,
        Disabled
    }

    [SerializeField] private float aimTime, chargeTime, fireTime;

    [SerializeField] private Transform targetPosition;
    [SerializeField] private Laser laser;
    [SerializeField] private Transform armTransform;
    [SerializeField] private ParticleSystem laserParticles;
    [SerializeField] [ReadOnly] private int repetitions;
    [SerializeField] [ReadOnly] private State currentState;

    private Animator _animator;
    private Vector3 _currentVelocity;
    private FinalBoss _finalBoss;
    private MovementController2D _movementController2D;
    private Transform _playerTransform;

    public StateMachine<State> Machine { get; private set; }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _movementController2D = GetComponent<MovementController2D>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _finalBoss = GetComponent<FinalBoss>();

        Machine = new StateMachine<State>(State.Disabled);

        InitWalkToTargetState();
        InitAimState();
        InitChargeState();
        InitFireState();
    }

    private void FixedUpdate()
    {
        currentState = Machine.CurrentState;
    }

    private void OnEnable()
    {
        repetitions = Random.Range(1, 5);
        Machine.CurrentState = State.WalkToTarget;
    }

    private void OnDisable()
    {
        laser.gameObject.SetActive(false);
        _animator.enabled = true;
        laserParticles.Stop();
    }

    private void InitFireState()
    {
        Machine.OnEnterState(State.Fire, () =>
        {
            laserParticles.Stop();
            laser.gameObject.SetActive(true);
            repetitions--;
            if (repetitions == 0) InvokeLater(() => _finalBoss.ChangeToRandomState(), 1f);
            else InvokeLater(() => Machine.CurrentState = State.Aim, 1f);
        });

        Machine.OnStayState(State.Fire, () =>
        {
            var direction = (_playerTransform.position - armTransform.position).normalized;
            armTransform.right = Vector3.Lerp(armTransform.right, direction, 0.0256f);
        });
    }

    private void InitChargeState()
    {
        Machine.OnEnterState(State.Charge, () =>
        {
            laserParticles.Play();
            InvokeLater(() => Machine.CurrentState = State.Fire, chargeTime);
        });
    }

    private void InitAimState()
    {
        Machine.OnEnterState(State.Aim, () =>
        {
            laser.gameObject.SetActive(false);
            InvokeLater(() => Machine.CurrentState = State.Charge, aimTime);
        });

        Machine.OnStayState(State.Aim, () =>
        {
            _animator.enabled = false;
            transform.right = Vector2.right;
            var direction = (_playerTransform.position - armTransform.position).normalized;
            armTransform.right = Vector3.SmoothDamp(armTransform.right, direction, ref _currentVelocity, 0.1f);
        });
    }

    private void InitWalkToTargetState()
    {
        Machine.OnEnterState(State.WalkToTarget, () => _animator.Play("walkRobot"));

        Machine.OnStayState(State.WalkToTarget, () =>
        {
            var distance = targetPosition.position - transform.position;
            _movementController2D.Move(Vector2.right * distance);
            if (distance.x * transform.right.x < 0) transform.Rotate(0f, 180f, 0f);
            if (!(Mathf.Abs(distance.x) > 2)) Machine.CurrentState = State.Aim;
        });

        Machine.OnExitState(State.WalkToTarget, () =>
        {
            _movementController2D.StopMoving();
            _animator.Play("nekai_laser");
        });
    }
}