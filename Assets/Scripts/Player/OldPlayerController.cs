using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(ProjectileShooter))]
public class OldPlayerController : MonoBehaviour
{
    //para el propulsor
    public delegate void JumpDelegate();

    public delegate void StopJumpDelegate();

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private ComboAttackController attackController;
    public bool canFly, canDash, canShoot, canAttack;
    public bool tankFilled = true;

    [SerializeField] private DialogueManager dialogueManager;

    private Animator _animator;

    private PlayerMovementController _movementController;
    private ProjectileShooter _projectileShooter;
    public JumpDelegate Jumping;
    public StopJumpDelegate StopJumping;

    public bool CanFly
    {
        get => canFly;
        set
        {
            _movementController.JumpStop();
            canFly = value;
        }
    }

    public void Start()
    {
        particles.Stop();
        _movementController = GetComponent<PlayerMovementController>();
        _projectileShooter = GetComponent<ProjectileShooter>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PropulsorTime.stopFly += StopJump;
    }

    private void OnDisable()
    {
        PropulsorTime.stopFly -= StopJump;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && canAttack) attackController.Attack();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && canDash)
        {
            _movementController.Dash();
            _animator.Play("player_tp");
        }
    }

    public void OnHeal(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if (CanFly) StartJump();
                else _movementController.JumpStart();
                break;
            case InputActionPhase.Canceled:
                if (CanFly) StopJump();
                else _movementController.JumpStop();
                break;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        if (value * transform.right.x < 0) Flip();
        _movementController.Move(value);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!canShoot) return;
        switch (context.phase)
        {
            case InputActionPhase.Started:
                _projectileShooter.StartShooting();
                _movementController.IsShooting = true;
                break;
            case InputActionPhase.Canceled:
                _projectileShooter.StopShooting();
                _movementController.IsShooting = false;
                break;
        }
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private void StartJump()
    {
        if (tankFilled)
        {
            _movementController.FlyStart();
            particles.Play();
            Jumping?.Invoke();
        }
    }

    private void StopJump()
    {
        _movementController.FlyStop();
        particles.Stop();
        StopJumping?.Invoke();
    }

    public void OnMenuDown(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnMenuUp(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnMenuSelect(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        dialogueManager.MenuSelect();
    }
}