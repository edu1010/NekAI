using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float Damage = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var health = collision.GetComponent<IDamageTaker>();
        if (health != null)
        {
            Debug.Log("HEALTH" + health);
            health.TakeDamage(Damage);
        }

        if (collision.tag == "tp1" || collision.tag == "tp2")
        {
            //Hcemos que no se destruya la bala con estos objetos
        }
        else
        {
            Destroy(gameObject);
        }
    }
}