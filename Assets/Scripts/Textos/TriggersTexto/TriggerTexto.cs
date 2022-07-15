using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTexto : MonoBehaviour
{
    public Conversation DialogueData;

    Collider2D collider2D;
    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {

            StartDialegue();
            gameObject.SetActive(false);    
        }
    }

    private void StartDialegue()
    {
        DialogueManager.Instance.StartConversation(DialogueData, gameObject);
    }
}
