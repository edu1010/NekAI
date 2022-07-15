using System;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public struct PlayerFlags
{
    public bool canDash, canFly, canAttack, canShoot;
}

[RequireComponent(typeof(MovementController2D))]
[RequireComponent(typeof(JumpController2D))]
[RequireComponent(typeof(DashController2D))]
[RequireComponent(typeof(FlightController2D))]
[RequireComponent(typeof(WallSlideController2D))]
[RequireComponent(typeof(ProjectileShooter))]
[RequireComponent(typeof(PlayerHealthSystem))]
public class PlayerController : ExtendedMonoBehaviour
{
    [SerializeField] private ComboAttackController attackController;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] [ReadOnly] private PlayerState currentPlayerState;
    public PlayerFlags playerFlags;

    private DashController2D _dashController;
    private FlightController2D _flightController;

    private InputManager _inputManager;
    private JumpController2D _jumpController;
    private MovementController2D _movementController;

    private PlayerHealthSystem _playerHealthSystem;
    private ProjectileShooter _projectileShooter;

    private Rigidbody2D _rigidbody;

    private StateMachine<PlayerState> _stateMachine;
    private WallSlideController2D _wallSlideController;
    private HealthBarController _healthBarController;

    private void Awake()
    {
        _healthBarController = GetComponent<HealthBarController>();
        _inputManager = FindObjectOfType<InputManager>();
        _movementController = GetComponent<MovementController2D>();
        _jumpController = GetComponent<JumpController2D>();
        _dashController = GetComponent<DashController2D>();
        _flightController = GetComponent<FlightController2D>();
        _wallSlideController = GetComponent<WallSlideController2D>();
        _projectileShooter = GetComponent<ProjectileShooter>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerHealthSystem = GetComponent<PlayerHealthSystem>();
        
        particleSystem.Stop();

        InitStateMachine();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.x * transform.right.x < -1 && !_wallSlideController.IsWallSliding)
        {
            Flip();
        }
        _stateMachine.CurrentState = GetPlayerState();
        currentPlayerState = _stateMachine.CurrentState;
        _stateMachine.Update();
    }

    private void OnEnable()
    {
        _dashController.OnPreDash += PreDash;
        _dashController.OnPostDash += PostDash;

        _wallSlideController.OnStartWallSliding += StartWallSliding;
        _wallSlideController.OnStopWallSliding += StopWallSliding;

        _inputManager.OnMoveRight += MoveRight;
        _inputManager.OnMoveLeft += MoveLeft;
        _inputManager.OnStopMoving += StopMoving;
        _inputManager.OnStartJumping += StartJumping;
        _inputManager.OnStopJumping += StopJumping;
        _inputManager.OnStartDash += Dash;
        _inputManager.OnStartAttack += Attack;
        _inputManager.OnStartShooting += StartShooting;
        _inputManager.OnStopShooting += StopShooting;
        _inputManager.OnStartHealing += StartHealing;
        _inputManager.OnStopHealing += StopHealing;

        _playerHealthSystem.OnPlayerDamaged += Damaged;
        _playerHealthSystem.OnPlayerRecovered += Recovered;
        _playerHealthSystem.OnPlayerDie += Die;

        _jumpController.GroundCheck.OnCollisionEnter += GroundChecked;
    }

    private void StopHealing()
    {
        _dashController.enabled =
            _flightController.enabled =
                _jumpController.enabled =
                    _movementController.enabled =
                        _projectileShooter.enabled =
                            _wallSlideController.enabled = true;
        
        _healthBarController.StopHealing();
    }

    private void StartHealing()
    {
        if (!_jumpController.JumpCheck.IsColliding) return;
        
        _dashController.enabled =
            _flightController.enabled =
                _jumpController.enabled =
                    _movementController.enabled =
                        _projectileShooter.enabled =
                            _wallSlideController.enabled = false;
        
        _healthBarController.StartHealing();
    }

    private void OnDisable()
    {
        _dashController.OnPreDash -= PreDash;
        _dashController.OnPostDash -= PostDash;

        _wallSlideController.OnStartWallSliding -= StartWallSliding;
        _wallSlideController.OnStopWallSliding -= StopWallSliding;

        _inputManager.OnMoveRight -= MoveRight;
        _inputManager.OnMoveLeft -= MoveLeft;
        _inputManager.OnStopMoving -= StopMoving;
        _inputManager.OnStartJumping -= StartJumping;
        _inputManager.OnStopJumping -= StopJumping;
        _inputManager.OnStartDash -= Dash;
        _inputManager.OnStartAttack -= Attack;
        _inputManager.OnStartShooting -= StartShooting;
        _inputManager.OnStopShooting -= StopShooting;
    }

    private void Damaged()
    {
        _dashController.enabled =
            _flightController.enabled =
                _jumpController.enabled =
                    _movementController.enabled =
                        _projectileShooter.enabled =
                            _wallSlideController.enabled = false;


        _stateMachine.CurrentState = PlayerState.Damaged;
        AudioManager.PlaySFX("hit");
        _stateMachine.Locked = true;
    }

    private void Die()
    {
        _dashController.enabled =
            _flightController.enabled =
                _jumpController.enabled =
                    _movementController.enabled =
                        _projectileShooter.enabled =
                            _wallSlideController.enabled = false;

        _stateMachine.CurrentState = PlayerState.Die;
        try { AudioManager.PlaySFX("die"); } catch (Exception e) { }
        
        _stateMachine.Locked = true;
    }

    private void Recovered()
    {
        _dashController.enabled =
            _flightController.enabled =
                _jumpController.enabled =
                    _movementController.enabled =
                        _projectileShooter.enabled =
                            _wallSlideController.enabled = true;

        _stateMachine.Locked = false;
    }

    private PlayerState GetPlayerState()
    {
        if (_jumpController.GroundCheck.IsColliding)
        {
            var x = _rigidbody.velocity.x;
            if (x < 1 && x > -1) return PlayerState.Idle;
            return PlayerState.Running;
        }

        if (_rigidbody.velocity.y < 0) return PlayerState.Falling;
        if (_rigidbody.velocity.y > 0) return playerFlags.canFly ? PlayerState.Flying : PlayerState.Jumping;
        return _stateMachine.CurrentState;
    }

    private void InitStateMachine()
    {
        _stateMachine = new StateMachine<PlayerState>(PlayerState.Idle);
        
        _stateMachine.OnEnterState(PlayerState.Idle, () => animator.Play("IdleCharacter"));
        _stateMachine.OnEnterState(PlayerState.Running, () => animator.Play("Run"));
        _stateMachine.OnEnterState(PlayerState.WallSliding, () => animator.Play("wallSliding"));
        _stateMachine.OnEnterState(PlayerState.Flying, () =>
        {
            animator.Play("fly");
            particleSystem.Play();
        });
        _stateMachine.OnExitState(PlayerState.Flying, () => particleSystem.Stop());
        _stateMachine.OnEnterState(PlayerState.IdleAttack1, () => animator.Play("attack_new"));
        _stateMachine.OnEnterState(PlayerState.IdleAttack2, () => animator.Play("attack_new"));
        _stateMachine.OnEnterState(PlayerState.IdleAttack3, () => animator.Play("attack_new"));
        _stateMachine.OnEnterState(PlayerState.AirAttack1, () => animator.Play("attack_new"));
        _stateMachine.OnEnterState(PlayerState.AirAttack2, () => animator.Play("attack_new"));
        _stateMachine.OnEnterState(PlayerState.AirAttack3, () => animator.Play("attack_new"));
        _stateMachine.OnEnterState(PlayerState.RunningAttack1, () => animator.Play("run_attack"));
        _stateMachine.OnEnterState(PlayerState.RunningAttack2, () => animator.Play("run_attack"));
        _stateMachine.OnEnterState(PlayerState.RunningAttack3, () => animator.Play("run_attack"));
        _stateMachine.OnEnterState(PlayerState.WallSlideAttack, () => animator.Play("character_falling_attacking"));
        _stateMachine.OnEnterState(PlayerState.Jumping, () => animator.Play("character_jump_up"));
        _stateMachine.OnEnterState(PlayerState.Falling, () => animator.Play("character_falling"));
        _stateMachine.OnEnterState(PlayerState.Die, () => animator.Play("IdleCharacter"));
        _stateMachine.OnEnterState(PlayerState.Teleporting, () => animator.Play("character_teleport"));
        _stateMachine.OnEnterState(PlayerState.IdleShoot, () => animator.Play("shoot"));
        _stateMachine.OnEnterState(PlayerState.RunningShoot, () => animator.Play("run_shoot"));
    
    }

    private void StopShooting()
    {
        if (!playerFlags.canShoot) return;
        _jumpController.enabled = true;
        _projectileShooter.StopShooting();
        _stateMachine.Locked = false;
    }

    private void StartShooting()
    {
        if (!playerFlags.canShoot) return;
        _projectileShooter.StartShooting();

        if (_jumpController.GroundCheck.IsColliding)
        {
            var x = _rigidbody.velocity.x;
            _stateMachine.CurrentState = x < 1 && x > -1 ? PlayerState.IdleShoot : PlayerState.RunningShoot;
        }
        else if (_wallSlideController.IsWallSliding)
        {
            _stateMachine.Locked = false;
            _stateMachine.CurrentState = PlayerState.WallSlideShoot;
        }
        else
        {
            _stateMachine.CurrentState = PlayerState.AirShoot;
        }

        _stateMachine.Locked = true;
    }

    private void Attack()
    {
        if (!playerFlags.canAttack) return;


        AudioManager.PlaySFX("sword");  
        var currentAttack = attackController.CurrentAttackIndex;

        if (_jumpController.GroundCheck.IsColliding)
        {
            UnityEngine.Debug.Log("GROUND AND ATTACKING");
            var x = _rigidbody.velocity.x;
            _stateMachine.CurrentState = x < 1 && x > -1
                ? currentAttack switch
                {
                    0 => PlayerState.IdleAttack1,
                    1 => PlayerState.IdleAttack2,
                    2 => PlayerState.IdleAttack3
                }
                : currentAttack switch
                {
                    0 => PlayerState.RunningAttack1,
                    1 => PlayerState.RunningAttack2,
                    2 => PlayerState.RunningAttack3
                };
        }
        else if (_wallSlideController.IsWallSliding)
        {
            _stateMachine.Locked = false;
            _stateMachine.CurrentState = PlayerState.WallSlideAttack;
            UnityEngine.Debug.Log("SLIDING AND ATTACKING");
        }
        else
        {
            UnityEngine.Debug.Log("FALLING AND ATTACKING");
            _stateMachine.CurrentState = currentAttack switch
            {
                0 => PlayerState.AirAttack1,
                1 => PlayerState.AirAttack2,
                2 => PlayerState.AirAttack3
            };
        }

        _stateMachine.Locked = true;

        var time = attackController.CurrentAttack.gameObject.GetComponent<Volatile>().LifeSpan;

        InvokeLater(() => _stateMachine.Locked = false, time);

        attackController.Attack();
    }

    private void StopWallSliding()
    {
        _jumpController.enabled = true;
        _stateMachine.Locked = false;
    }

    private void StartWallSliding()
    {
        _jumpController.enabled = false;
        _stateMachine.CurrentState = PlayerState.WallSliding;
        _stateMachine.Locked = true;
    }

    private void PostDash()
    {
        _movementController.enabled =
            _jumpController.enabled = _projectileShooter.enabled =
                attackController.enabled = _wallSlideController.enabled = _flightController.enabled = true;
        _stateMachine.Locked = false;
    }

    private void PreDash()
    {
        _movementController.enabled =
            _jumpController.enabled = _projectileShooter.enabled =
                attackController.enabled = _wallSlideController.enabled = _flightController.enabled = false;
        _stateMachine.CurrentState = PlayerState.Teleporting;
        _stateMachine.Locked = true;
    }

    private void Dash()
    {
        if (!playerFlags.canDash) return;
        _dashController.Dash();
        AudioManager.PlaySFX("dash");
    }

    private void StopJumping()
    {

        if (playerFlags.canFly){
            _flightController.StopFlying();
            AudioManager.StopSFX();
        } 
        else _jumpController.StopJumping();
    }

    private void StartJumping()
    {
        if (playerFlags.canFly){
            AudioManager.PlaySFX("jetpack");
            _flightController.StartFlying();
        }   
        else
            _jumpController.StartJumping();
    }

    private void StopMoving()
    {
        _movementController.StopMoving();
    }

    private void MoveLeft()
    {
        if (transform.right.x * Vector2.left.x < 0 && !_wallSlideController.IsWallSliding) Flip();
        _movementController.MoveLeft();
    }

    private void MoveRight()
    {
        if (transform.right.x * Vector2.right.x < 0 && !_wallSlideController.IsWallSliding) Flip();
        _movementController.MoveRight();
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private void GroundChecked(Collider2D coll){
        AudioManager.PlaySFX("fall");
    }

    private enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Flying,
        WallSliding,
        Teleporting,
        Damaged,
        Die,
        IdleAttack1,
        IdleAttack2,
        IdleAttack3,
        RunningAttack1,
        RunningAttack2,
        RunningAttack3,
        AirAttack1,
        AirAttack2,
        AirAttack3,
        WallSlideAttack,
        IdleShoot,
        RunningShoot,
        AirShoot,
        WallSlideShoot
    }
}