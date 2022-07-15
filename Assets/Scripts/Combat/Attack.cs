using UnityEngine;

[RequireComponent(typeof(BasicDamageDealer))]
public class Attack : MonoBehaviour
{
    [SerializeField] private Vector2 attackForce = Vector2.zero;

    private BasicDamageDealer _damageDealer;

    public void Awake()
    {
        _damageDealer = GetComponent<BasicDamageDealer>();
    }

    public void OnEnable()
    {
        _damageDealer.OnDealDamage += OnHit;
    }

    public void OnDisable()
    {
        _damageDealer.OnDealDamage -= OnHit;
    }

    private void OnHit(IDamageTaker damageTaker)
    {
        var component = (MonoBehaviour) damageTaker;
        var rb = component.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = new Vector2(transform.right.x * attackForce.x, attackForce.y);
    }
}