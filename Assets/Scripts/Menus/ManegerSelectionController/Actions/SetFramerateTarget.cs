using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetFramerateTarget : MonoBehaviour,ISlider
{
    [SerializeField]
    Slider slider;

    private void Start()
    {
        slider.value = GameFlowController.Instance.GetFpsTarget();
        GameFlowController.Instance.ChangeFPSTarget(60);
    }
    public void DecrementSlider()
    {
        if(slider.value - 1f > slider.minValue)
        {
            slider.value = slider.value - 1f;
            GameFlowController.Instance.ChangeFPSTarget((int)slider.value);
        }
    }

    public void IncrementSlider()
    {
        Debug.Log("Incrementa");
        if (slider.value + 1f < slider.maxValue)
        {
            slider.value = slider.value + 1f;
            GameFlowController.Instance.ChangeFPSTarget((int)slider.value);
        }
    }
}
