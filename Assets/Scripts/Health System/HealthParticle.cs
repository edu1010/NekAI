using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthParticle : MonoBehaviour
{
    [SerializeField] private Range<float> randomSpeed, randomTime;
    [SerializeField] private CollisionDetector2D playerDetector;

    private float _finalTime, _enabledTime;
    private Vector2 _currentVelocity;
    private Rigidbody2D _rigidbody2D;
    private Transform target;

    private void Awake()
    {
        playerDetector = GetComponent<CollisionDetector2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (Time.time > _enabledTime + _finalTime)
        {
            var direction = (Vector2)(target.position - transform.position).normalized;
            var velocity = _rigidbody2D.velocity.magnitude;
            _rigidbody2D.velocity = direction * (velocity + 30f * Time.fixedDeltaTime);
        }
        else
        {
            _rigidbody2D.velocity = Vector2.SmoothDamp(_rigidbody2D.velocity, Vector2.zero, ref _currentVelocity, _finalTime);
        }
        
    }

    private void OnEnable()
    {
        _rigidbody2D.velocity = transform.right * Random.Range(randomSpeed.min, randomSpeed.max);
        _finalTime = Random.Range(randomTime.min, randomTime.max);
        playerDetector.OnCollisionEnter += Disable;
        _enabledTime = Time.time;
    }

    private void OnDisable()
    {
        AudioManager.PlaySFX("receive_heal");
        playerDetector.OnCollisionEnter -= Disable;
    }

    private void Disable(Collider2D obj)
    {
        gameObject.SetActive(false);
    }
}