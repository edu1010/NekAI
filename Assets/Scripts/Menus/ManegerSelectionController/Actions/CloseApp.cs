using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : MonoBehaviour,IRow
{
    public void ToDo()
    {
        GameFlowController.Instance.FinishGame();

    }
}
