using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseConf : MonoBehaviour,IRow
{
    public void ToDo()
    {
        GameFlowController.Instance.ExitSettingsGame();

    }
}
