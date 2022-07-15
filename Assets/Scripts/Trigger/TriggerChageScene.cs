using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChageScene : MonoBehaviour
{
    [SerializeField]
    GoTo nextScene = GoTo.NextLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            switch (nextScene)
            {
                case (GoTo.midGame):
                    GameFlowController.Instance.GoToMidGame();
                    break;
                case (GoTo.NextLevel):
                    GameFlowController.Instance.PasarLVL();
                    break;
                case (GoTo.BackToFistEvent):
                    GameFlowController.Instance.BackToFistEvent();
                    break;
                case (GoTo.BackToDash):
                    GameFlowController.Instance.BackToDash();
                    break; 
                case (GoTo.GoToBoos):
                    GameFlowController.Instance.GoToBoos();
                    break; 
                case (GoTo.ToNekAi):
                    GameFlowController.Instance.GoToNekAi();
                    break;
            }
                
            
        }
    }
}

public enum GoTo
{
    midGame,
    BackToFistEvent,
    BackToDash,
    GoToBoos,
    NextLevel,
    ToNekAi
}