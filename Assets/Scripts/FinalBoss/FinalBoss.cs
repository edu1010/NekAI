using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(FinalBossStompBehaviour))]
[RequireComponent(typeof(FinalBossLaserBehaviour))]
public class FinalBoss : ExtendedMonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] [ReadOnly] private State currentState;
    private Animator _animator;
    private BasicDamageTaker _basicDamageTaker;
    private FinalBossLaserBehaviour _finalBossLaserBehaviour;
    private FinalBossShootBehaviour _finalBossShootBehaviour;
    private FinalBossStompBehaviour _finalBossStompBehaviour;
    private StateMachine<State> _stateMachine;

    private void Awake()
    {
        _basicDamageTaker = GetComponentInChildren<BasicDamageTaker>();
        _animator = GetComponentInChildren<Animator>();

        _finalBossShootBehaviour = GetComponent<FinalBossShootBehaviour>();
        _finalBossLaserBehaviour = GetComponent<FinalBossLaserBehaviour>();
        _finalBossStompBehaviour = GetComponent<FinalBossStompBehaviour>();

        _stateMachine = new StateMachine<State>(State.Idle);

        _stateMachine.OnEnterState(State.Idle, () => InvokeLater(ChangeToRandomState, 3f));
        _stateMachine.OnStayState(State.Idle, () => _animator.Play("idleRobot"));

        _stateMachine.OnEnterState(State.Stomp, () => _finalBossStompBehaviour.enabled = true);
        _stateMachine.OnStayState(State.Stomp, () => _finalBossStompBehaviour.Machine.Update());
        _stateMachine.OnExitState(State.Stomp, () => _finalBossStompBehaviour.enabled = false);

        _stateMachine.OnEnterState(State.Laser, () => _finalBossLaserBehaviour.enabled = true);
        _stateMachine.OnStayState(State.Laser, () => _finalBossLaserBehaviour.Machine.Update());
        _stateMachine.OnExitState(State.Laser, () => _finalBossLaserBehaviour.enabled = false);

        _stateMachine.OnEnterState(State.Shoot, () => _finalBossShootBehaviour.enabled = true);
        _stateMachine.OnStayState(State.Shoot, () => _finalBossShootBehaviour.Machine.Update());
        _stateMachine.OnExitState(State.Shoot, () => _finalBossShootBehaviour.enabled = false);
    }
    
    private void FixedUpdate()
    {
        _stateMachine.Update();
        currentState = _stateMachine.CurrentState;
    }

    private void OnEnable()
    {
        _finalBossStompBehaviour.enabled = false;
        _finalBossLaserBehaviour.enabled = false;
        _finalBossShootBehaviour.enabled = false;

        _basicDamageTaker.OnDie += OnDie;

        var t = transform;
        t.position = new Vector2(startPosition.position.x, t.position.y);
        
        _stateMachine.CurrentState = State.Idle;
    }

    private void OnDie(BasicDamageTaker obj)
    {
        gameObject.SetActive(false);
    }

    public void ChangeToRandomState()
    {
        State state;
        do
        {
            state = (State) Random.Range(0, Enum.GetValues(typeof(State)).Length);
        } while (state == State.Idle || state == _stateMachine.CurrentState);

        _stateMachine.CurrentState = state;
    }

    private enum State
    {
        Idle,
        Stomp,
        Shoot,
        Laser
    }
}