using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredDamage : MonoBehaviour
{
    
    BasicDamageTaker damageTaker;

    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    float enterTransitionTime;

    [SerializeField]
    float exitTransitionTime;

    float constantEnterTime,constantExitTime;

    [SerializeField]
    Color targetColor;

    [SerializeField]
    Color defaultColor;

    private bool executeAnimation, toRedCompleted;

    void Awake()
    {
        this.damageTaker = GetComponent<BasicDamageTaker>();

        executeAnimation = false;
        toRedCompleted = false;

        constantEnterTime = enterTransitionTime;
        constantExitTime = exitTransitionTime;
    }

    void OnEnable(){
        damageTaker.OnTakeDamage += EntityReceivedDamage;
    }

    void OnDisable(){
        damageTaker.OnTakeDamage -= EntityReceivedDamage;
    }

    void Update()
    {
        if(executeAnimation){
            if(toRedCompleted)
                transitionToDefault();
            else 
                transitionToRed();
            
        }
    }

    void EntityReceivedDamage(BasicDamageTaker bdt){
        executeAnimation = true;
    }

    private void transitionToRed(){
        if (enterTransitionTime <= Time.deltaTime)
        {
            sprite.color = targetColor;
            enterTransitionTime = constantEnterTime;
            toRedCompleted = true;
        }
        else
        {
            sprite.color = Color.Lerp(sprite.material.color, targetColor, Time.deltaTime / enterTransitionTime);
            enterTransitionTime -= Time.deltaTime;
        }
    }

    private void transitionToDefault(){
        if (exitTransitionTime <= Time.deltaTime)
        {
            sprite.color = defaultColor;
            exitTransitionTime = constantExitTime;

            toRedCompleted = false;
            executeAnimation = false;
            
        }
        else
        {
            sprite.color = Color.Lerp(sprite.material.color, defaultColor, Time.deltaTime / exitTransitionTime);
            exitTransitionTime -= Time.deltaTime;
        }
    }
}
