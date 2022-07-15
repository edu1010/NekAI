using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLaser : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    ScriptableObject DialogueData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("Open", true);
            StartDialegue();
            gameObject.SetActive(false);
        }
       

    }
    private void StartDialegue()
    {
        if (DialogueData is LongConversation)
        {
            DialogueManager.Instance.StartConversationLong((LongConversation)DialogueData, gameObject);
        }
        if (DialogueData is ConversationMedium)
        {
            DialogueManager.Instance.StartConversationMedium((ConversationMedium)DialogueData, gameObject);
        }
        if (DialogueData is Conversation)
        {
            DialogueManager.Instance.StartConversation((Conversation)DialogueData, gameObject);
        }
    }

}
