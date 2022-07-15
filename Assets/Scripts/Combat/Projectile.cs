using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BasicDamageDealer))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 20f;

    private BasicDamageDealer damageDealer;
    private Rigidbody2D rb;

    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    public void Awake()
    {
        damageDealer = GetComponent<BasicDamageDealer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnEnable()
    {
        rb.velocity = transform.right * movementSpeed;
        damageDealer.CollisionDetector.OnCollisionEnter += Disable;
    }

    public void OnDisable()
    {
        damageDealer.CollisionDetector.OnCollisionEnter -= Disable;
    }

    private void Disable(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}