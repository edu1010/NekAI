using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBossDie : MonoBehaviour
{
    [SerializeField]
    BasicDamageTaker damageTaker;
    private void OnEnable()
    {
        damageTaker.OnDie += GoToCredits;
    }
    void GoToCredits(BasicDamageTaker basicDamageTaker)
    {
        GameFlowController.Instance.PasarLVL();
    }
}
