using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageTaker
{
    public delegate void OnDeathDelegate();

    public delegate void OnHitDelegate(float fraction);

    [SerializeField] public float maxHealth = 100.0f;

    public bool invencible; //añadido para segunda fase waverBoss
    public float CurrentHealthCopia;
    public OnDeathDelegate OnDeath;
    public OnHitDelegate OnHit;
    public float MaxHealth => maxHealth;

    public float CurrentHealth { get; set; }

    public bool Dead { get; private set; }
    public Action OnPlayerDie { get; internal set; }

    protected virtual void Start()
    {
        CurrentHealth = maxHealth;
        Dead = false;
    }

    private void Update()
    {
        CurrentHealthCopia = CurrentHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        if (!invencible)
        {
            CurrentHealth -= amount;

            OnHit?.Invoke(CurrentHealth / MaxHealth);
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
            if (CurrentHealth <= 0.0f && !Dead) OnDeath?.Invoke();
        }
    }

    public virtual void Die()
    {
        OnDeath?.Invoke();
        Dead = true;
        Destroy(gameObject);
    }
}