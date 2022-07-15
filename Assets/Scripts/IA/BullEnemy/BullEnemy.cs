using UnityEngine;

[RequireComponent(typeof(MovementController2D))]
public class BullEnemy : MonoBehaviour
{
    [SerializeField] private float prepareChargeTime;
    [SerializeField] private CollisionDetector2D playerDetector, wallCheck;
    [SerializeField] private Animator animator;

    private Vector2 bullDirection;
    private FSM<State> fsm;

    private MovementController2D movementController;
    private Transform playerTransform;

    public void Awake()
    {
        movementController = GetComponent<MovementController2D>();
        InitFSM();
    }

    public void FixedUpdate()
    {
        fsm.Update();
    }

    public void OnEnable()
    {
        playerDetector.OnCollisionEnter += OnPlayerDetected;
        playerDetector.OnCollisionExit += OnPlayerLost;
        wallCheck.OnCollisionEnter += OnWallHit;

        var dmgTaker = GetComponent<BasicDamageTaker>();
        dmgTaker.OnTakeDamage += HealthLost;
    }

    public void OnDisable()
    {
        playerDetector.OnCollisionEnter -= OnPlayerDetected;
        playerDetector.OnCollisionExit -= OnPlayerLost;
        wallCheck.OnCollisionEnter -= OnWallHit;
    }

    private void OnWallHit(Collider2D obj)
    {
        if (fsm.GetState() == State.Charge) fsm.SetState(State.EndingCharge);
    }

    private void InitFSM()
    {
        fsm = new FSM<State>(State.Idle);

        fsm.OnEnterState(State.Idle, () =>
        {
            movementController.StopMoving();
            animator.Play("idleBull");
        });

        fsm.OnEnterState(State.PreparingCharge, () =>
        {
            PrepareCharge();
            animator.Play("transformBull");
        });

        fsm.OnEnterState(State.EndingCharge, () =>
        {
            movementController.StopMoving();
            //PLAY BULL STOPPING ANIMATION!
            Invoke("PrepareCharge", 0.5f);
        });

        fsm.OnStayState(State.PreparingCharge, () => movementController.StopMoving());

        fsm.OnEnterState(State.Charge, () => animator.Play("attackBull"));

        fsm.OnStayState(State.Charge, () =>
        {
            var xDistance = Mathf.Abs(gameObject.transform.position.x - playerTransform.position.x);
            if (bullDirection.x > 0) movementController.MoveRight();
            if (bullDirection.x < 0) movementController.MoveLeft();

            if (xDistance < 0.25f) Invoke("StopCharging", 0.3f);
        });
    }

    private void OnPlayerDetected(Collider2D collider)
    {
        if (fsm.GetState() == State.Idle)
        {
            playerTransform = collider.gameObject.transform;
            fsm.SetState(State.PreparingCharge);
        }
    }

    private void OnPlayerLost(Collider2D collider)
    {
        fsm.SetState(State.Idle);
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }


    public void PrepareCharge()
    {
        bullDirection = playerTransform.position - transform.position;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 direction = playerTransform.position - transform.position;

        if (direction.x * transform.right.x < 0) Flip();
        movementController.StopMoving();

        //Play loading CHARGE ANIMATION!

        Invoke("Charge", prepareChargeTime);
    }

    public void Charge()
    {
        fsm.SetState(State.Charge);
    }

    public void StopCharging()
    {
        fsm.SetState(State.EndingCharge);
    }

    private void HealthLost(BasicDamageTaker damageTaker)
    {


        var player = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform = player;
        if (fsm.GetState() == State.Charge) return;
        fsm.SetState(State.PreparingCharge);
    }

    private enum State
    {
        PreparingCharge,
        Charge,
        EndingCharge,
        Idle
    }
}