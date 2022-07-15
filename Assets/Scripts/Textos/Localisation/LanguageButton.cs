﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageButton : MonoBehaviour, IPointerClickHandler
{
    public Language Language;
    public void OnPointerClick(PointerEventData eventData)
    {
        Localizator.SetLanguage(Language);
    }
}
