using System;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector2D))]
public class Collectible : MonoBehaviour
{
    private CollisionDetector2D _collisionDetector2D;
    
    [SerializeField]
    ScriptableObject DialogueData;

    private void Awake()
    {
        _collisionDetector2D = GetComponent<CollisionDetector2D>();
    }

    private void OnEnable()
    {
        _collisionDetector2D.OnCollisionEnter += OnCollected;
    }

    private void OnDisable()
    {
        _collisionDetector2D.OnCollisionEnter -= OnCollected;
    }

    public event Action OnCollectedEvent;

    private void OnCollected(Collider2D collider2D1)
    {
        OnCollectedEvent?.Invoke();
        StartDialegue();
        gameObject.SetActive(false);
    }


    private void StartDialegue()
    {
        if(DialogueData is LongConversation)
        {
            DialogueManager.Instance.StartConversationLong((LongConversation)DialogueData, gameObject);
        }
        if(DialogueData is ConversationMedium)
        {
            DialogueManager.Instance.StartConversationMedium((ConversationMedium)DialogueData, gameObject);
        }
        if(DialogueData is Conversation)
        {
            DialogueManager.Instance.StartConversation((Conversation)DialogueData, gameObject);
        }
    }
}