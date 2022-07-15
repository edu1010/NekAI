using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour, IRow
{
    public void ToDo()
    {
        GameFlowController.Instance.StartGame();
    }

}
