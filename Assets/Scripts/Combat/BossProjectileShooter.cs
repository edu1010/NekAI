using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(ProjectileShooter))]
public class BossProjectileShooter : MonoBehaviour
{
    private ProjectileShooter _projectileShooter;

    private Vector2 _originalDirection;
    private float _currentRotation, _targetRotation, _originalRotation;

    private void Awake()
    {
        var t = transform;
        _originalDirection = t.right;
        _originalRotation = t.rotation.eulerAngles.z;
        _targetRotation = _originalRotation;
        _projectileShooter = GetComponent<ProjectileShooter>();
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0f, 180f, _originalRotation + 75);
        StartCoroutine(LoopChange());
        _projectileShooter.StartShooting();
    }

    private void OnDisable()
    {
        _projectileShooter.StopShooting();
    }

    private IEnumerator LoopChange()
    {
        while (true)
        {
            _targetRotation = _originalRotation + 75;
            yield return new WaitForSeconds(1);
            _targetRotation = _originalRotation - 75;
            yield return new WaitForSeconds(1);
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 180f,
            Mathf.SmoothDamp(transform.rotation.eulerAngles.z, _targetRotation, ref _currentRotation, 1f));
        if (transform.rotation.eulerAngles.z < _originalRotation - 75 ||
            transform.rotation.eulerAngles.z > _originalRotation + 75)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, _originalRotation);
        }
    }
}
