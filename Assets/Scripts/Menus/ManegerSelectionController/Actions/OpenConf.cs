using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenConf : MonoBehaviour, IRow
{
    public void ToDo()
    {
        GameFlowController.Instance.OpenSettingsGame();
        
    }
}
