using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private CollisionDetector2D collisionDetector2D;
    [SerializeField] private BasicDamageTaker basicDamageTaker;

    [SerializeField] [ReadOnly] private float healthBar;
    [SerializeField] [ReadOnly] private bool isHealing;

    private Rigidbody2D _rigidbody2D;
    private float _healingCounter = 0f;

    public float HealthBar
    {
        get => healthBar;
        set => healthBar = value;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        collisionDetector2D.OnCollisionEnter += HealthParticleCollected;
    }

    private void HealthParticleCollected(Collider2D obj)
    {
        if (healthBar >= 100) return;
        HealthBar++;
    }

    public void StartHealing()
    {
        isHealing = true;
    }

    public void StopHealing()
    {
        isHealing = false;
    }

    private void FixedUpdate()
    {
        if (!isHealing) return;
        _rigidbody2D.velocity = Vector2.zero;
        if (healthBar <= 0) return;
        healthBar--;
        _healingCounter++;
        if (!(_healingCounter >= 50)) return;
        basicDamageTaker.CurrentHealth += 1;
        _healingCounter = 0;
    }
}
