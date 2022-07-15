using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    public List<ParticleCollisionEvent> collisionEvents;
    float health = 1f;


    public static bool particlesIsActive = false;
    // Start is called before the first frame update
    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        _particleSystem.Pause();
    }

    private void OnEnable()
    {
        ParticleInvokerOnDIe.OnPlay += Play;
    }



    //Solo un enemigo te puede curar simultaneamente,al morir este mueve el sistema de particulas y lo invoca
    public void Play(Transform pos)
    {
        particlesIsActive = true;
        transform.position = pos.position;
        _particleSystem.Play();
        StartCoroutine(ParticulePause());
        
    }

    private void OnParticleCollision(GameObject other)
    {
        int nCollisionEvents = _particleSystem.GetCollisionEvents(other, collisionEvents);

        var hs = other.GetComponent<BasicDamageTaker>();
        for (int i = 0; i < nCollisionEvents; i++)
        {
            if (other.tag.Equals("Player"))
            {
                hs.TakeDamage(-health);
                _particleSystem.Stop();
                
            }
        }
    }
    //Dèspues de curarnos en el siguiente segundo no podra darnos vida los enemigos
    private IEnumerator ParticulePause()
    {
        yield return new WaitForSeconds(1f);
        particlesIsActive = false;
    }
}
