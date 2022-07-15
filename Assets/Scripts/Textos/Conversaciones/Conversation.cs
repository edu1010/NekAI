    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New Conversation",menuName ="Dialogue/Conversation/Short",order =2)]
public class Conversation : ScriptableObject
{
    public string name;
    public string Key;
    public DialogueNode StartNode;
}

