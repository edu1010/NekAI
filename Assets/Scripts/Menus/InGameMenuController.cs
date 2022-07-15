using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuController : MonoBehaviour
{
    public MenuPanel Default;
    public MenuPanel Conf;
    [SerializeField]
    int indexNextRowOnOpenSettings = 3;
    public void SetPaused()
    {
        SelectManager.Instance.ChangeMenu(indexNextRowOnOpenSettings);
        SelectManager.Instance.ActualCanvasG = CanvasG.canvasgrup2;
        Default.Hide();
        Conf.Show();
    } 
    public void UnSetPaused()
    {
        SelectManager.Instance.ActualCanvasG = CanvasG.canvasgrup1;
        Default.Show();
        Conf.Hide();
    }

    public void OpenSettings()
    {
        SelectManager.Instance.ChangeMenu(indexNextRowOnOpenSettings);
        SelectManager.Instance.ActualCanvasG = CanvasG.canvasgrup2;
        Default.Hide();
        Conf.Show();
    } 
    public void ExitSettings()
    {
        SelectManager.Instance.ChangeMenu(0);
        SelectManager.Instance.ActualCanvasG = CanvasG.canvasgrup1;
        SelectManager.Instance.ChangeMenu(0);
        SelectManager.Instance.ActualCanvasG = CanvasG.canvasgrup1;
        Default.Show();
        Conf.Hide();
    }
}
