using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangeLanguaje : MonoBehaviour, ISlider
{
    [SerializeField]
    Language language;
    [SerializeField]
    TextMeshProUGUI _text;
    int index = 1;

    void Start()
    {
        //_text.text = Enum.GetName(typeof(Language),index);

    }
    public void DecrementSlider()
    {
        index = changeIndex(index - 1);
        //_text.text = Enum.GetName(typeof(Language), index);
        language = (Language)index;
        Localizator.SetLanguage(language);
    }

    public void IncrementSlider()
    {
        index = changeIndex(index + 1);
       // _text.text = Enum.GetName(typeof(Language), index);
        language = (Language)index;
        Localizator.SetLanguage(language);
    }


    int changeIndex(int _index)
    {
      
        if (_index < 1)
        {
            _index = 3;
        }
        if (_index > 3)
        {
            _index = 1;
        }
        return _index;
    }
  
}
