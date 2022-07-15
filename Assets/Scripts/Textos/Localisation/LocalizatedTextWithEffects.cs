using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LocalizatedTextWithEffects : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public string Key;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Localizator.OnLanguageChangeDelegate += OnLanguageChanged;
        SetText();
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
        //SetText();
    }

    private void SetText()
    {
        _text.text = "";
        StartCoroutine(textEffect(Localizator.GetText(Key)));
    }


    IEnumerator textEffect(String text)
    {
        foreach (var character in text)
        {

            _text.text = _text.text + character;
            yield return new WaitForSeconds(0.02f);
        }

    }
}
