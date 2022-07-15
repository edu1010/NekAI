using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoNothing", menuName = "Dialogue/EndNodes", order = 2)]
public class EndNode : DialogueNode
{
    public virtual void OnChosen(GameObject talker)
    {

    }
}
