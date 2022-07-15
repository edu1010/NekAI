using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public string Key;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Localizator.OnLanguageChangeDelegate += OnLanguageChanged;
    }

    private void OnDisable()
    {
        Localizator.OnLanguageChangeDelegate -= OnLanguageChanged;
    }

    private void OnLanguageChanged()
    {
        SetText();
    }

    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        SetText();
    }

    private void SetText()
    {
        _text.text = Localizator.GetText(Key);
    }

    
}
