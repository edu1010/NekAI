using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderController : MonoBehaviour
{
    private BasicDamageTaker _basicDamageTaker;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _basicDamageTaker = GameObject.FindGameObjectWithTag("Player").GetComponent<BasicDamageTaker>();
    }

    private void Update()
    {
        _slider.value = _basicDamageTaker.CurrentHealth;
    }
}
