using System;
using UnityEngine;
using UnityEngine.UI;

public class JetPackSlider : MonoBehaviour
{
    [SerializeField] private float sliderSmoothing;
    public Gradient ColorGradient;
    public Image fillImage;
    private FlightController2D _flightController2D;
    private Slider _slider;
    float currentAlpha;

    Color aux;

    private float _targetValue, _currentValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _flightController2D = FindObjectOfType<FlightController2D>();
   
        aux = fillImage.color;
    }

    private void FixedUpdate()
    {
        fillImage.fillAmount = Mathf.SmoothDamp(fillImage.fillAmount, _targetValue, ref _currentValue, sliderSmoothing);
       
        if(Math.Abs(fillImage.fillAmount - 1) < 0.01f)
        {
            fillImage.color = new Color(aux.r, aux.g, aux.b, 
                Mathf.SmoothDamp(fillImage.color.a, 0  , ref currentAlpha, sliderSmoothing));
        }
        else
        {
            fillImage.color = new Color(aux.r, aux.g, aux.b, 
                Mathf.SmoothDamp(fillImage.color.a, 1, ref currentAlpha, sliderSmoothing));
        }
    }

    private void OnEnable()
    {
        _flightController2D.OnFuelUpdate += SetValue;
    }

    private void OnDisable()
    {
        _flightController2D.OnFuelUpdate -= SetValue;
    }

    private void SetValue(float f)
    {
        _targetValue = f;
        //FillImage.color = ColorGradient.Evaluate(f);
        //FillImage.fillAmount = f;
    }
}