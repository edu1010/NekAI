using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MovementController2D))]
public class FinalBossBehaviour : ExtendedMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform armTransform;
    [SerializeField] private ParticleSystem laserParticles;
    [SerializeField] private Laser laser;
    [SerializeField] private BossProjectileShooter bossProjectileShooter;

    private StateMachine<ActionState> _actionStateMachine;
    private StateMachine<BossState> _bossStateMachine;
    private MovementController2D _movementController2D;

    private Transform _playerTransform;
    private Rigidbody2D _rigidbody2D;

    private Vector3 currentVelocity;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _movementController2D = GetComponent<MovementController2D>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        InitActionStateMachine();
        InitBossStateMachine();

        _bossStateMachine.CurrentState = BossState.Idle;
        laserParticles.Stop();
        laser.gameObject.SetActive(false);
    }

    private void Start()
    {
        bossProjectileShooter.enabled = false;
    }

    private void LateUpdate()
    {
        _actionStateMachine.Update();
    }

    private void InitActionStateMachine()
    {
        _actionStateMachine = new StateMachine<ActionState>(ActionState.Idle);

        _actionStateMachine.OnEnterState(ActionState.Idle, () =>
        {
            _movementController2D.StopMoving();
            animator.Play("idleRobot");
            InvokeLater(() => _bossStateMachine.CurrentState = GetNextBossState(), 2f);
        });

        _actionStateMachine.OnEnterState(ActionState.WalkToPlayer, () => animator.Play("walkRobot"));

        _actionStateMachine.OnStayState(ActionState.WalkToPlayer, () =>
        {
            var distance = _playerTransform.position - transform.position;
            if (Mathf.Abs(distance.x) <= 6)
                _actionStateMachine.CurrentState = ActionState.Stomp;
            else
                _movementController2D.Move(new Vector2(distance.x, 0));
            if (_rigidbody2D.velocity.x * transform.right.x < -0.1f) transform.Rotate(0f, 180f, 0f);
        });

        _actionStateMachine.OnEnterState(ActionState.Stomp, () =>
        {
            _movementController2D.StopMoving();
            animator.Play("stampAttackRobot");
            InvokeLater(() => _bossStateMachine.CurrentState = GetNextBossState(), 1f);
        });

        _actionStateMachine.OnEnterState(ActionState.WalkToPosition, () => animator.Play("walkRobot"));

        _actionStateMachine.OnStayState(ActionState.WalkToPosition, () =>
        {
            var direction = startPosition.position - transform.position;
            if (Mathf.Abs(direction.x) <= 1)
                _actionStateMachine.CurrentState = _bossStateMachine.CurrentState == BossState.Shoot
                    ? ActionState.Shoot
                    : ActionState.Laser;
            else
                _movementController2D.Move(new Vector2(direction.x, 0));
            if (_rigidbody2D.velocity.x * transform.right.x < -0.1f) transform.Rotate(0f, 180f, 0f);
        });

        _actionStateMachine.OnEnterState(ActionState.Shoot, () =>
        {
            bossProjectileShooter.enabled = true;
            _movementController2D.StopMoving();
            animator.Play("nekai_shoot");
            InvokeLater(() => _bossStateMachine.CurrentState = GetNextBossState(), Random.Range(5f, 15f));
        });

        _actionStateMachine.OnStayState(ActionState.Shoot, () => { animator.enabled = false; });

        _actionStateMachine.OnExitState(ActionState.Shoot, () =>
        {
            bossProjectileShooter.enabled = false;
            animator.enabled = true;
        });

        _actionStateMachine.OnStayState(ActionState.Shoot, () => { transform.right = Vector2.right; });

        ActionStateMachineLaserState();
    }

    private void ActionStateMachineLaserState()
    {
        var laserStateMachine = new StateMachine<LaserStates>(LaserStates.Aim);
        var shots = 0;

        laserStateMachine.OnEnterState(LaserStates.Aim, () =>
        {
            laser.gameObject.SetActive(false);
            InvokeLater(() => laserStateMachine.CurrentState = LaserStates.Charge, 1f);
        });

        laserStateMachine.OnStayState(LaserStates.Aim, () =>
        {
            var direction = (_playerTransform.position - armTransform.position).normalized;
            armTransform.right = Vector3.SmoothDamp(armTransform.right, direction, ref currentVelocity, 0.1f);
        });

        laserStateMachine.OnEnterState(LaserStates.Charge, () =>
        {
            laserParticles.Play();
            InvokeLater(() => laserStateMachine.CurrentState = LaserStates.Fire, 0.75f);
        });

        laserStateMachine.OnEnterState(LaserStates.Fire, () =>
        {
            laserParticles.Stop();
            laser.gameObject.SetActive(true);
            shots--;
            if (shots == 0) InvokeLater(() => _bossStateMachine.CurrentState = GetNextBossState(), 1f);
            else InvokeLater(() => laserStateMachine.CurrentState = LaserStates.Aim, 1f);
        });

        laserStateMachine.OnStayState(LaserStates.Fire, () =>
        {
            var direction = (_playerTransform.position - armTransform.position).normalized;
            armTransform.right = Vector3.Lerp(armTransform.right, direction, 0.0256f);
        });

        _actionStateMachine.OnEnterState(ActionState.Laser, () =>
        {
            shots = Random.Range(2, 6);
            _movementController2D.StopMoving();
            animator.Play("nekai_laser");
            laserStateMachine.CurrentState = LaserStates.Aim;
        });

        _actionStateMachine.OnStayState(ActionState.Laser, () =>
        {
            animator.enabled = false;
            transform.right = Vector2.right;
            laserStateMachine.Update();
        });

        _actionStateMachine.OnExitState(ActionState.Laser, () =>
        {
            laser.gameObject.SetActive(false);
            animator.enabled = true;
            laserParticles.Stop();
        });
    }

    private void InitBossStateMachine()
    {
        _bossStateMachine = new StateMachine<BossState>(BossState.Idle);

        _bossStateMachine.OnEnterState(BossState.Idle, () => _actionStateMachine.CurrentState = ActionState.Idle);
        _bossStateMachine.OnEnterState(BossState.Stomp,
            () => _actionStateMachine.CurrentState = ActionState.WalkToPlayer);
        _bossStateMachine.OnEnterState(BossState.Shoot,
            () => _actionStateMachine.CurrentState = ActionState.WalkToPosition);
        _bossStateMachine.OnEnterState(BossState.Laser,
            () => _actionStateMachine.CurrentState = ActionState.WalkToPosition);
    }

    private BossState GetNextBossState()
    {
        BossState bossState;
        do
        {
            bossState = (BossState) Random.Range(0, Enum.GetValues(typeof(BossState)).Length);
        } while (bossState == BossState.Idle);

        return bossState;
    }

    private enum LaserStates
    {
        Aim,
        Charge,
        Fire
    }

    private enum BossState
    {
        Idle,
        Stomp,
        Shoot,
        Laser
    }

    private enum ActionState
    {
        Idle,
        WalkToPlayer,
        Stomp,
        WalkToPosition,
        Shoot,
        Laser
    }
}