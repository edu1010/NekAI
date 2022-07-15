using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthParticlePoolerBoss : MonoBehaviour
{
   [SerializeField] private float minParticles, maxParticles;
   [SerializeField] private BasicDamageTaker basicDamageTaker;
   [SerializeField]
   CollisionDetector2D collisionDetector2D;

    [SerializeField]
    GameObject espada1;
    [SerializeField]
    GameObject espada2;
    [SerializeField]
    GameObject espada3;
    private ObjectPooler _objectPooler;

    private void Awake()
    {
        _objectPooler = GetComponent<ObjectPooler>();
    }

    private void OnEnable()
    {
        //basicDamageTaker.OnDie += OnDie;
        collisionDetector2D.OnCollisionEnter += StartParticles;
    }
    private void StartParticles(Collider2D collision)
    {
        Debug.Log("Colision");
        if (collision.gameObject.Equals(espada1) || collision.gameObject.Equals(espada2) || collision.gameObject.Equals(espada3))
        {
            var amount = Random.Range(minParticles, maxParticles);
            for (var i = 0; i < amount; i++)
            {
                var particle = _objectPooler.GetFromPool();
                particle.transform.position = transform.position;
                particle.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
                particle.SetActive(true);
            }

        }
    }
        
}

