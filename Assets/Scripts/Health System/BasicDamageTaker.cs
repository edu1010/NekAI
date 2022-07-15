using System;
using UnityEngine;

public class BasicDamageTaker : MonoBehaviour, IDamageTaker
{
    [SerializeField] private float maxHealth;
    [SerializeField] private bool isInvincible;
    [SerializeField] [ReadOnly] private float currentHealth;

    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    public bool IsInvincible
    {
        get => isInvincible;
        set => isInvincible = value;
    }

    public void Awake()
    {
        OnDie += damageTaker => damageTaker.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ResetHealth();
    }

    public void Die()
    {
        OnDie?.Invoke(this);
        OnDieS.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if(gameObject.tag != "Player") AudioManager.PlaySFX("hit");

        if (isInvincible) return;
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
        else OnTakeDamage?.Invoke(this);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void ClearOnDieEventSubscriptions()
    {
        OnDie = null;
    }

    public event Action<BasicDamageTaker> OnDie;
    public static event Action OnDieS;

    public event Action<BasicDamageTaker> OnTakeDamage;
}