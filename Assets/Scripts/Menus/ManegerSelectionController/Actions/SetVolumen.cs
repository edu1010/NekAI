    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolumen : MonoBehaviour, ISlider
{
    [SerializeField]
    Slider slider;
    

    private void Start()
    {
        
    }
    public void DecrementSlider()
    {
        slider.value = slider.value - 0.01f;
    }

    public void IncrementSlider()
    {
        slider.value = slider.value + 0.01f;
    }
}
