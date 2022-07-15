using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Row : MonoBehaviour
{
    public CanvasG canvasGroup;
    public int position;
    public bool selected = false;
    public Image image;
    public bool isLang = false;
    public void Select()
    {
        selected = true;
        image.enabled = true;
    }
    public void Diselect()
    {
        selected = false;
        image.enabled = false;
    }
    
}
public enum CanvasG
{
    canvasgrup1,
    canvasgrup2
}
