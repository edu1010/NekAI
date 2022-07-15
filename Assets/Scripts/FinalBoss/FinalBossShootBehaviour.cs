using UnityEngine;

[RequireComponent(typeof(MovementController2D))]
[RequireComponent(typeof(FinalBoss))]
public class FinalBossShootBehaviour : ExtendedMonoBehaviour
{
    public enum State
    {
        WalkToTarget,
        Shoot,
        Disabled
    }

    [SerializeField] private Range<float> randomShootingTime;

    [SerializeField] private Transform targetPosition;
    [SerializeField] private BossProjectileShooter bossProjectileShooter;

    private Animator _animator;
    private FinalBoss _finalBoss;
    private MovementController2D _movementController2D;

    public StateMachine<State> Machine { get; private set; }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _movementController2D = GetComponent<MovementController2D>();
        _finalBoss = GetComponent<FinalBoss>();

        Machine = new StateMachine<State>(State.Disabled);

        InitWalkToTargetState();
        InitShootState();

        bossProjectileShooter.enabled = false;
    }

    private void OnEnable()
    {
        Machine.CurrentState = State.WalkToTarget;
    }

    private void OnDisable()
    {
        _animator.enabled = true;
        bossProjectileShooter.enabled = false;
    }

    private void InitShootState()
    {
        Machine.OnEnterState(State.Shoot, () =>
        {
            bossProjectileShooter.enabled = true;
            _animator.Play("nekai_shoot");
            _movementController2D.StopMoving();
            InvokeLater(() => _finalBoss.ChangeToRandomState(),
                Random.Range(randomShootingTime.min, randomShootingTime.max));
        });

        Machine.OnStayState(State.Shoot, () =>
        {
            _animator.enabled = false;
            transform.right = Vector2.right;
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
            if (!(Mathf.Abs(distance.x) > 2)) Machine.CurrentState = State.Shoot;
        });
    }
}