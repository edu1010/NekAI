using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadScene : MonoBehaviour
{
    private BasicDamageTaker _basicDamageTaker;

    private void Awake()
    {
        _basicDamageTaker = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicDamageTaker>();
    }

    private void OnEnable()
    {
        _basicDamageTaker.ClearOnDieEventSubscriptions();
        _basicDamageTaker.OnDie += Reload;
    }

    private void OnDisable()
    {
        _basicDamageTaker.OnDie -= Reload;
    }

    private void Reload(BasicDamageTaker obj)
    {
        GameFlowController.Instance.reiniciarUltimoLVL();
    }
}
