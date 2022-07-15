using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHell : ExtendedMonoBehaviour
{
    private StateMachine<State> _stateMachine;

    float _timerInState;
    float timerInState=2;
    InvokerBullets invokerBullets;
    float ratio = 0.2f;
    float ratioCircle = 1f;
    float increment = 0;
    Vector3 posWip;
    GameObject path;
    Transform[] positions;
    int maxPath;
    int nextPath;
    float timerWip = 2;
    int counterTimesWip;
    [SerializeField] 
    float timePhase=30;
    
    
    GameObject TriggerAnimation;
    GameObject colectible;

    private MovementController2D _movementController;

    private void Awake()
    {
        path = GameObject.Find("PathBulletHell");
        TriggerAnimation = GameObject.Find("TriggerFinishBulletHellEvent");
        invokerBullets=GetComponent<InvokerBullets>();
        _movementController = GetComponent<MovementController2D>();
        posWip = transform.position;
        maxPath = path.GetComponentsInChildren<Transform>().Length;
        positions = path.GetComponentsInChildren<Transform>();
        colectible = GameObject.FindGameObjectWithTag("EngineBulllet");
        colectible.SetActive(false);


        InvokeLater((() => {
            Debug.Log("30 segundos");
            colectible.SetActive(true);
            colectible.GetComponent<Collider2D>().enabled = true;
            //TriggerAnimation.GetComponent<Collider2D>().enabled=true;
            Destroy(gameObject);
        }), timePhase);
        InitFsm();

    }
    private void FixedUpdate()
    {
        _stateMachine.Update();
    }
    private void InitFsm()
    {
        transform.position = positions[0].position;
        _stateMachine = new StateMachine<State>(State.Circle);
        _stateMachine.OnEnterState(State.Circle, () => {
            ratioCircle = 1;
            increment = 0;
            _timerInState = timerInState;

        });
        _stateMachine.OnStayState(State.Circle,()=> 
        {
            increment += Time.deltaTime;
            _timerInState -= Time.deltaTime;
            if (increment > ratioCircle)
            {
                increment = 0;
                invokerBullets.StartShootCircles();
            }
            if (_timerInState < 0)
            {
                _stateMachine.CurrentState = State.MoveCircle;
            }

        });
        _stateMachine.OnEnterState(State.MoveCircle, () => {
            if (nextPath == null)
            {
                nextPath = 0;
            }
            else
            {
                nextPath += 1;
                if(nextPath> maxPath-1)
                {
                    nextPath = 0;
                    _stateMachine.CurrentState = State.WipLeft;

                }
            }
        });
        _stateMachine.OnStayState(State.MoveCircle, () =>
        {
            transform.position = positions[nextPath].position;
            _stateMachine.CurrentState = State.Circle;
        });

        _stateMachine.OnEnterState(State.WipLeft, ()=> {
            counterTimesWip = 0;
            timerWip = 2;
            transform.position = posWip; 
        });
        _stateMachine.OnEnterState(State.WipRight, ()=> {
            counterTimesWip = 0;
            timerWip = 2;
            transform.position = posWip; });
        _stateMachine.OnEnterState(State.WipLR, ()=> {
            counterTimesWip = 0;
            timerWip = 2.5f;
            transform.position = posWip;
            StartCoroutine(DoubleWhip());
            StartCoroutine(MoveUpAndDown());
        });
        
        _stateMachine.OnStayState(State.WipLeft, () => 
        {
            
            if (timerWip < 0 || timerWip ==2)
            {
                invokerBullets.StartShootLeft();
                timerWip = 2;
                counterTimesWip += 1;
                if (counterTimesWip >= 2)
                {
                    _stateMachine.CurrentState = State.WipRight;
                }
            }
            timerWip -= Time.deltaTime;

        });
        _stateMachine.OnStayState(State.WipRight, () => 
        {
            if (timerWip < 0 || timerWip == 2)
            {
                invokerBullets.StartShootRight();
                timerWip = 2;
                counterTimesWip += 1;
                if (counterTimesWip >= 2)
                {
                    _stateMachine.CurrentState = State.WipLR;
                }
            }
            timerWip -= Time.deltaTime;

        
        });
        
        _stateMachine.CurrentState = State.Circle;
    }

    private IEnumerator DoubleWhip()
    {
        while (_stateMachine.CurrentState == State.WipLR)
        {
            invokerBullets.StartDouleWip();
            yield return new WaitForSeconds(timerWip);
        }
    }

    private IEnumerator MoveUpAndDown()
    {
        while (_stateMachine.CurrentState == State.WipLR)
        {
            _movementController.MoveDown();
            yield return new WaitForSeconds(1.5f);
           
            _movementController.MoveUp();
            yield return new WaitForSeconds(1.5f);
            
            _movementController.MoveDown();
            yield return new WaitForSeconds(1.5f);
          
            _movementController.MoveUp();
            yield return new WaitForSeconds(1.5f);
            
            _movementController.MoveLeft();
            yield return new WaitForSeconds(2f);
            
            _movementController.MoveRight();
            yield return new WaitForSeconds(2f);
           
        }
    }

    private enum State
    {
        Circle,
        WipLeft,
        WipRight,
        WipLR,
        MoveCircle
    }
}
