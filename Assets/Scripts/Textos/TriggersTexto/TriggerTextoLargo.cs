using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTextoLargo : MonoBehaviour
{
    public LongConversation DialogueData;

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
        DialogueManager.Instance.StartConversationLong(DialogueData, gameObject);
    }
}
