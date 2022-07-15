using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation Long", menuName = "Dialogue/Conversation/Long", order = 2)]
public class LongConversation : ScriptableObject
{
    public string name;
    public string Key;
    public DialogueNode StartNode;
    public DialogueNode SecondNode;
    public DialogueNode LastNode;

}




