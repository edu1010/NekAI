using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation Medium", menuName = "Dialogue/Conversation/Medium", order = 2)]
public class ConversationMedium : ScriptableObject
{
    public string name;
    public string Key;
    public DialogueNode StartNode;
    public DialogueNode SecondNode;
}

