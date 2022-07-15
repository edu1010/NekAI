using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    
    private void Update()
    {
        DoLaser();
    }

    private void OnEnable()
    {
        DoLaser();
    }

    private void DoLaser()
    {
        var hit = Physics2D.Raycast(transform.position, transform.right);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hit.point);
        var damageTaker = hit.rigidbody?.gameObject.GetComponent<IDamageTaker>();
        damageTaker?.TakeDamage(1);
    }
}
