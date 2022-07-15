using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicDamageTaker))]
[RequireComponent(typeof(MovementController2D))]
public class Walker : ExtendedMonoBehaviour
{
    [SerializeField] private float damagedStopTime;
    [SerializeField] private CollisionDetector2D groundCheck, wallCheck, edgeCheck;

    private BasicDamageTaker _damageTaker;
    private MovementController2D _movementController;

    private StateMachine<State> _stateMachine;

    private enum State {Patrol, Damaged, Die}

    private void Awake()
    {
        _movementController = GetComponent<MovementController2D>();
        _damageTaker = GetComponent<BasicDamageTaker>();

        InitStateMachine();
    }

    private void FixedUpdate()
    {
        _stateMachine.Update();
    }

    private void OnEnable()
    {
        wallCheck.OnCollisionEnter += Flip;
        edgeCheck.OnCollisionExit += Flip;

        _damageTaker.OnTakeDamage += Damaged;
        _damageTaker.OnDie += Die;

        _stateMachine.Locked = false;
        _stateMachine.CurrentState = State.Patrol;
    }

    private void Die(BasicDamageTaker obj)
    {
        _stateMachine.CurrentState = State.Die;
    }

    private void Damaged(BasicDamageTaker obj)
    {
        _stateMachine.CurrentState = State.Damaged;
        AudioManager.PlaySFX("hit");
    }

    private void OnDisable()
    {
        wallCheck.OnCollisionEnter -= Flip;
        edgeCheck.OnCollisionExit -= Flip;
        
        _damageTaker.OnTakeDamage -= Damaged;
        _damageTaker.OnDie -= Die;
    }

    private void Flip(Collider2D obj)
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private void InitStateMachine()
    {
        _stateMachine = new StateMachine<State>(State.Patrol);
        
        _stateMachine.OnStayState(State.Patrol, () =>
        {
            _movementController.MoveForward();
            _movementController.enabled = groundCheck.IsColliding;
        });
        
        _stateMachine.OnEnterState(State.Damaged, () =>
        {
            _stateMachine.Locked = true;
            _movementController.enabled = false;
            Invoke(nameof(Recover), damagedStopTime);
        });
        
        _stateMachine.OnExitState(State.Damaged, () => _movementController.enabled = true);
        
        _stateMachine.OnEnterState(State.Die, () =>
        {
            CancelInvoke(nameof(Recover));
            _stateMachine.Locked = true;
            _movementController.enabled = false;
        });
    }

    private void Recover()
    {
        _stateMachine.Locked = false;
        _stateMachine.CurrentState = State.Patrol;
    }
}
