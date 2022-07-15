using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EyeEnemy : ExtendedMonoBehaviour
{
    [SerializeField] private PlayerDetector playerDetector;

    [SerializeField] private Transform eyeTransform;

    [SerializeField] private float aimSmoothingTime = 0.25f;
    private float _currentRotation, _targetRotation, _originalRotation;
    private Vector2 _originalDirection;

    private Vector2 currentVelocity;

    private FSM<State> fsm;
    private Transform playerTransform;

    private ProjectileShooter projectileShooter;


    public void Awake()
    {
        InitFSM();

        projectileShooter = GetComponent<ProjectileShooter>();
        _originalDirection = eyeTransform.up;
        _originalRotation = eyeTransform.rotation.eulerAngles.z;
        _targetRotation = _originalRotation;
    }

    public void FixedUpdate()
    {
        fsm.Update();
    }

    public void OnEnable()
    {
        playerDetector.OnPlayerDetected += OnPlayerDetected;
        playerDetector.OnPlayerLost += OnPlayerLost;

        var dmgTaker = GetComponent<BasicDamageTaker>();
        dmgTaker.OnTakeDamage += HealthLost;
    }

    public void OnDisable()
    {
        playerDetector.OnPlayerDetected -= OnPlayerDetected;
        playerDetector.OnPlayerLost -= OnPlayerLost;
    }

    private void InitFSM()
    {
        fsm = new FSM<State>(State.Idle);

        fsm.OnEnterState(State.Idle, RotateRandom);

        fsm.OnStayState(State.Idle,
            () =>
            {
                eyeTransform.rotation = Quaternion.Euler(0f, 0f,
                    Mathf.SmoothDamp(eyeTransform.rotation.eulerAngles.z, _targetRotation, ref _currentRotation, 0.7f));
            });

        fsm.OnEnterState(State.Attack, () => { Invoke("StartShootingBullets", 0.7f); });

        fsm.OnExitState(State.Attack, () => { projectileShooter.StopShooting(); });

        fsm.OnStayState(State.Attack, () =>
        {
            var targetDirection = playerTransform.position - eyeTransform.position;
            var angle = Vector2.SignedAngle(_originalDirection, targetDirection) + _originalRotation;
            angle = Mathf.Clamp(angle, _originalRotation - 50f, _originalRotation + 50f);
            angle = Mathf.SmoothDamp(eyeTransform.rotation.eulerAngles.z, angle, ref _currentRotation, aimSmoothingTime);

            eyeTransform.rotation = Quaternion.Euler(0f, 0f, angle);

            //eyeTransform.up = Vector2.SmoothDamp(eyeTransform.up,eyeFocus,ref currentVelocity,0.7f);
        });

        fsm.SetState(State.Idle);
    }

    private void RotateRandom()
    {
        if (fsm.GetState() != State.Idle) return;
        InvokeLater(() =>
        {
            _targetRotation = Random.Range(_originalRotation - 50f, _originalRotation + 50f);
            RotateRandom();
        }, Random.Range(0.5f, 3f));
    }

    private void OnPlayerDetected(Transform t)
    {
        playerTransform = t;
        fsm.SetState(State.Attack);
    }

    private void OnPlayerLost()
    {
        fsm.SetState(State.Idle);
    }


    private void HealthLost(BasicDamageTaker damageTaker)
    {
        var player = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform = player;

        fsm.SetState(State.Attack);
    }

    private void StartShootingBullets()
    {
        if (fsm.GetState() == State.Attack)
            projectileShooter.StartShooting();
    }

    private enum State
    {
        Idle,
        Attack
    }
}