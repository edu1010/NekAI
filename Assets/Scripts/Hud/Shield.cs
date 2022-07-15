using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Shield : MonoBehaviour
{
    [SerializeField]
    HealthBarController barController;
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _text.text = barController.HealthBar + " %";
    }
}
