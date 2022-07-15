using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowActualFramerate : MonoBehaviour
{
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "60";
    }

    public void OnChangeValue()
    {
        _text.text = GameFlowController.Instance.GetFpsTarget().ToString();
    }
}
