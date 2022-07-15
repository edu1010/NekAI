using System;
using UnityEngine;

[RequireComponent(typeof(BasicDamageTaker))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerHealthSystem : ExtendedMonoBehaviour
{
    [SerializeField] private float onDamagedTime, damageInvincibleTime, onDieTime;

    [SerializeField] private Transform checkPoint;

    private BasicDamageTaker _damageTaker;
    private Rigidbody2D _rigidbody;
    private PlayerFlicker _playerFlicker;

    public Transform CheckPoint
    {
        get => checkPoint;
        set => checkPoint = value;
    }

    private void Awake()
    {
        _playerFlicker = GetComponent<PlayerFlicker>();
        _damageTaker = GetComponent<BasicDamageTaker>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _damageTaker.OnTakeDamage += OnHit;
        _damageTaker.ClearOnDieEventSubscriptions();
        _damageTaker.OnDie += OnDie;
    }

    private void OnDisable()
    {
        _damageTaker.OnTakeDamage -= OnHit;
        _damageTaker.OnDie -= OnDie;
    }

    public event Action OnPlayerDamaged, OnPlayerRecovered, OnPlayerDie;

    private void OnHit(BasicDamageTaker obj)
    {
        OnPlayerDamaged?.Invoke();
        _rigidbody.velocity = Vector2.zero;
        _damageTaker.IsInvincible = true;
        _playerFlicker.StartFlicker();
        InvokeLater(() => OnPlayerRecovered?.Invoke(), onDamagedTime);
        InvokeLater(() =>
        {
            _damageTaker.IsInvincible = false;
            _playerFlicker.StopFLicker();
        }, damageInvincibleTime);
    }

    private void OnDie(BasicDamageTaker basicDamageTaker)
    {
        OnPlayerDie?.Invoke();
        if (checkPoint is { }) transform.position = checkPoint.position;
        _rigidbody.velocity = Vector2.zero;
        _damageTaker.IsInvincible = true;
        _damageTaker.ResetHealth();
        InvokeLater(() => OnPlayerRecovered?.Invoke(), onDieTime);
        InvokeLater(() => _damageTaker.IsInvincible = false, damageInvincibleTime);
    }
}