using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBarController : MonoBehaviour
{
    [SerializeField] private float smoothingTime;
    
    private Slider _slider;
    private HealthBarController _healthBarController;
    private float currentVelocity;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _healthBarController = FindObjectOfType<HealthBarController>();
    }

    private void FixedUpdate()
    {
        _slider.value = Mathf.SmoothDamp(_slider.value, _healthBarController.HealthBar, ref currentVelocity,
            smoothingTime);
    }
}
