using System;
using UnityEngine;

public class BasicDamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private CollisionDetector2D collisionDetector;

    public CollisionDetector2D CollisionDetector => collisionDetector;

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    private void OnEnable()
    {
        collisionDetector.OnCollisionEnter += DealDamage;
    }

    private void OnDisable()
    {
        collisionDetector.OnCollisionEnter -= DealDamage;
    }

    public event Action<IDamageTaker> OnDealDamage;

    private void DealDamage(Collider2D collision)
    {
        var damageTaker = collision.gameObject.GetComponent<IDamageTaker>();
        if (damageTaker == null) return;
        damageTaker.TakeDamage(damage);
        OnDealDamage?.Invoke(damageTaker);
    }
}