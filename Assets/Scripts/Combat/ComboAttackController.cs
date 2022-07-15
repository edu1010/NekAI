using System;
using UnityEngine;

public class ComboAttackController : ExtendedMonoBehaviour
{
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float comboResetTime;

    [SerializeField] [ReadOnly] private int currentAttackIndex;
    [SerializeField] [ReadOnly] private bool canAttack;

    private Attack[] _attacks;

    public int CurrentAttackIndex => currentAttackIndex;

    public Attack CurrentAttack => _attacks[currentAttackIndex];

    private void Awake()
    {
        _attacks = GetComponentsInChildren<Attack>(true);
        canAttack = true;
        currentAttackIndex = 0;
    }

    public void Start()
    {
        foreach (var attack in _attacks) attack.gameObject.SetActive(false);
    }

    public event Action OnAttackStart, OnAttackEnd;

    public void Attack()
    {
        if (!canAttack || _attacks.Length == 0) return;
        CancelInvoke(nameof(ResetCombo));
        var attack = _attacks[CurrentAttackIndex];
        OnAttackStart?.Invoke();
        attack.gameObject.SetActive(true);
        InvokeLater(() => OnAttackEnd?.Invoke(), attack.gameObject.GetComponent<Volatile>().LifeSpan);
        canAttack = false;
        currentAttackIndex = CurrentAttackIndex + 1;
        if (CurrentAttackIndex == _attacks.Length) ResetCombo();
        InvokeLater(() => canAttack = true, timeBetweenAttacks);
        Invoke(nameof(ResetCombo), comboResetTime);
    }

    private void ResetCombo()
    {
        currentAttackIndex = 0;
    }
}