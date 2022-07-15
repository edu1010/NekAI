using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private bool isMachineGun;
    [SerializeField] private float shotsPerSecond;
    [SerializeField] private Transform projectileSource;
    [SerializeField] private ObjectPooler projectilePool;

    [SerializeField] [ReadOnly] private float timer;
    [SerializeField] [ReadOnly] private bool isShooting;

    public bool IsShooting
    {
        get => isShooting;
        set => isShooting = value;
    }

    public void FixedUpdate()
    {
        if (!IsShooting) return;
        if (isMachineGun)
        {
            timer += Time.fixedDeltaTime;
            if (!(timer > 1 / shotsPerSecond)) return;
            Shoot();
            timer = 0;
        }
        else
        {
            Shoot();
            IsShooting = false;
        }
    }

    public void StartShooting()
    {
        timer = shotsPerSecond;
        IsShooting = true;
    }

    public void StopShooting()
    {
        IsShooting = false;
    }

    public void Shoot()
    {
        if(gameObject.tag.Equals("Player")){
            AudioManager.PlaySFX("shoot_gun");
        }else{
            AudioManager.PlaySFX("enemy_shoot");    
        }
        
        var obj = projectilePool.GetFromPool();
        if (!obj) return;
        obj.transform.position = projectileSource.position;
        obj.transform.rotation = projectileSource.rotation;
        obj.SetActive(true);
    }
}