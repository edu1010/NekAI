
using UnityEngine;


public class LaserEnemy : Weapon
{
    public Transform upFirepoint;
    public Transform downFirepoint;
    public Transform leftFirepoint;
    public Transform rightFirepoint;
    public LineRenderer _lineRenderer;
    public bool up=true;
    public bool left=false;
    public float range = 100.0f;
    float Damage=1.0f;
    Transform m_transform;
    void Awake()
    {

        m_transform = GetComponent<Transform>();
    }
    void Update()
    {
        Fire();
    }
    public override void Fire()
    {
        chooseRay();   
    }
    public void chooseRay()
    {

        RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.up);

        if (up)
        {
            _hit = Physics2D.Raycast(m_transform.position, transform.up, 100);
            if (_hit)
            {
                
                Draw2DRay(upFirepoint.position, _hit.point);
                DamageRay(_hit.rigidbody);
            }
            else
            {
                Draw2DRay(upFirepoint.position, upFirepoint.transform.up * range);
            }
        }
        if (left)
        {
            if (_hit)
            {
                _hit = Physics2D.Raycast(m_transform.position, transform.right * -1f, 100);
                Draw2DRay(upFirepoint.position, _hit.point);
                DamageRay(_hit.rigidbody);
            }
            else
            {
                Draw2DRay(upFirepoint.position, upFirepoint.transform.up * range);
            }
        }
        
    }

    void Draw2DRay(Vector2 startPosition, Vector2 endPosition)
    {
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);
    }
    void DamageRay(Rigidbody2D raycastHit)
    {
        if (raycastHit == null) return;
        BasicDamageTaker health = raycastHit.GetComponent<BasicDamageTaker>();
        if (health != null && health.transform.gameObject.tag == "Player")
        {
            Debug.Log("HEALTH" + health);
            health.TakeDamage(Damage);
        }
    }

}


