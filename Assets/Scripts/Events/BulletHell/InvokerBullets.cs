using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokerBullets : MonoBehaviour
{

    [SerializeField]
    float ratio = 0.2f;
    float ratioCircle = 1f;
    float increment=0;
    [SerializeField]
    int AmoutOfShoots=20;
    //private Transform projectileSource;
    [SerializeField]
    private ObjectPooler projectilePool;
    private void Start()
    {
        
    }
   
    public void StartShootCircles()
    {
        ShootCicle();
    }
    public void StartShootLeft()
    {
        StartCoroutine(ShootGoToLeft(10));
    } 
    public void StartShootRight()
    {
        StartCoroutine(ShootGoToRight(10));
    } 
    public void StartDouleWip()
    {
        StartCoroutine(ShootGoToLeft(10));
        StartCoroutine(ShootGoToRight(10));
    }

    void ShootCicle()
    {
        float degree = 360/AmoutOfShoots;
        for (int i = 0; i < AmoutOfShoots; i++)
        {
            
            Shoot(Quaternion.Euler(0,0,degree*i));
        }
    }

    IEnumerator ShootGoToRight(int degree)
    {
        for (int i = 0; i < 20; i++)
        {

            Shoot(Quaternion.Euler(0, 0, 180+degree * i));
            yield return new WaitForSeconds(ratio);
        }

    }
    IEnumerator  ShootGoToLeft(int degree)
    {
        for (int i = 0; i < 20; i++)
        {

            Shoot(Quaternion.Euler(0, 0,  -degree * i));
            yield return new WaitForSeconds(ratio);
        }

    }

    public void Shoot(Quaternion rotation)
    {
        var obj = projectilePool.GetFromPool();
        if (!obj) return;
        obj.transform.position = transform.position;
        obj.transform.rotation = rotation;
        obj.SetActive(true); 
    }
}
