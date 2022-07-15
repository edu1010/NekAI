using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DashController2D : ExtendedMonoBehaviour
{
    [SerializeField] private float dashDistance = 4, preDashTime = 0.2f, postDashTime, cooldown = 0.5f;

    [SerializeField] private RaycastDetector2D raycastDetector;

    [SerializeField] [ReadOnly] private bool canDash;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        canDash = true;
    }

    public event Action OnPreDash, OnDash, OnPostDash, OnDashReloaded;

    public void Dash()
    {
        if (!canDash) return;
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        OnPreDash?.Invoke();
        canDash = false;
        _rigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(preDashTime);
        var (collided, distance) = raycastDetector.GetDistanceToCollisionLayers();
        var t = transform;
        t.position = t.position + t.right * (collided ? distance : dashDistance);
        OnDash?.Invoke();
        yield return new WaitForSeconds(postDashTime);
        OnPostDash?.Invoke();
        InvokeLater(() =>
        {
            canDash = true;
            OnDashReloaded?.Invoke();
        }, cooldown);
    }
}