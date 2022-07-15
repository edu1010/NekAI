using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController2D))]
public class SuicidalEnemy : MonoBehaviour
{
    enum State { Chase, Idle, Suicide }

    [SerializeField] CollisionDetector2D playerDetector, edgeCheck, wallCheck;
    [SerializeField] GameObject explosionDetector;
    MovementController2D movementController;
    FSM<State> fsm;
    Transform playerTransform;

    Animator animator;
    public float maxEnemyDistance;

    public void Awake()
    {
        movementController = GetComponent<MovementController2D>();
        animator = GetComponent<Animator>();
        InitFSM();
    }

    public void OnEnable()
    {
        playerDetector.OnCollisionEnter += OnPlayerDetected;
        playerDetector.OnCollisionExit += OnPlayerLost;
        edgeCheck.OnCollisionExit += OnEdgeReached;
        wallCheck.OnCollisionEnter += OnWallReached;

        BasicDamageTaker dmgTaker = GetComponent<BasicDamageTaker>();
        dmgTaker.OnTakeDamage += HealthLost;

    }

    private void OnWallReached(Collider2D obj)
    {
        fsm.SetState(State.Idle);
    }

    private void OnEdgeReached(Collider2D obj)
    {
        fsm.SetState(State.Idle);
    }

    public void OnDisable()
    {
        playerDetector.OnCollisionEnter -= OnPlayerDetected;
        playerDetector.OnCollisionExit -= OnPlayerLost;
        edgeCheck.OnCollisionExit -= OnEdgeReached;
        wallCheck.OnCollisionEnter -= OnWallReached;
    }

    public void FixedUpdate()
    {
        fsm.Update();
    }

    void InitFSM()
    {
        fsm = new FSM<State>(State.Idle);

        fsm.OnEnterState(State.Idle, () =>
        {
            movementController.StopMoving();
            animator.Play("idleSuicida");
        });

        fsm.OnEnterState(State.Suicide, () =>
        {
            animator.Play("attackSuicida");
            Suicide();
            
        });

        fsm.OnEnterState(State.Chase, () =>
        {
            animator.Play("walkSuicida");
        });

        fsm.OnStayState(State.Suicide,() => movementController.StopMoving());

        fsm.OnStayState(State.Chase, () =>
        {
            Vector2 direction = playerTransform.position - transform.position;
    
            if (direction.x * transform.right.x < 0) Flip();
            if (direction.x > 0) movementController.MoveRight();
            if (direction.x < 0) movementController.MoveLeft();
            if (direction.x == 0) movementController.StopMoving();

            if(direction.magnitude < maxEnemyDistance){
                fsm.SetState(State.Suicide);
            }
        });
    }

    void OnPlayerDetected(Collider2D collider)
    {

        if(fsm.GetState() != State.Suicide){
            playerTransform = collider.gameObject.transform;
            fsm.SetState(State.Chase);
        }

    }

    void OnPlayerLost(Collider2D collider)
    {
        playerTransform = null;
        fsm.SetState(State.Idle);
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    void Suicide(){

        //Play Suicide animation

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0,1000));
        Invoke("Explode", 0.45f);
        
    }

    void Explode(){
        animator.Play("explode");
        explosionDetector.SetActive(true);
        AudioManager.PlaySFX("explode");
        Destroy(gameObject,0.25f);
    }

    void HealthLost(BasicDamageTaker damageTaker){
        
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        playerTransform = player;

        fsm.SetState(State.Chase);
    }
}
