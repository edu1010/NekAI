using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInvokerOnDIe : MonoBehaviour
{
    BasicDamageTaker damageTaker;
    [SerializeField]
    float probabilityToHealth = 0.25f;
    // Start is called before the first frame update
    void Awake()
    {
        damageTaker = GetComponent<BasicDamageTaker>();
    }

    void OnEnable()
    {
        damageTaker.OnDie += EntityDie;
    }

    void OnDisable()
    {
        damageTaker.OnDie -= EntityDie;
    }
    void EntityDie(BasicDamageTaker bdt)
    {
        if (Particle.particlesIsActive == false)
        {
            if (UnityEngine.Random.value <= probabilityToHealth)
            {
                //Invocamos las particulas
                OnPlay?.Invoke(this.transform);
            }
        }
    }
    public static event Action<Transform> OnPlay;
}
