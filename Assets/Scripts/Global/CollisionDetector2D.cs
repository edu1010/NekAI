using System;
using UnityEngine;

public class CollisionDetector2D : MonoBehaviour
{
    [Tooltip("Collider used to detect collisions.")] [SerializeField]
    private new Collider2D collider;

    [Tooltip("Layer masks to detect collisions.")] [SerializeField]
    private LayerMask layerMasks;

    [SerializeField] [ReadOnly] private bool isColliding;
    public bool IsColliding => isColliding;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEvent(EventType.Enter, collision.collider);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CollisionEvent(EventType.Exit, collision.collider);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CollisionEvent(EventType.Stay, collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionEvent(EventType.Enter, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CollisionEvent(EventType.Exit, collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CollisionEvent(EventType.Stay, collision);
    }

    public event Action<Collider2D> OnCollisionEnter;
    public event Action<Collider2D> OnCollisionStay;
    public event Action<Collider2D> OnCollisionExit;

    private void CollisionEvent(EventType eventType, Collider2D collision)
    {
        if (!MatchLayerMask(collision)) return;
        switch (eventType)
        {
            case EventType.Enter:
                isColliding = true;
                OnCollisionEnter?.Invoke(collision);
                break;
            case EventType.Stay:
                isColliding = true;
                OnCollisionStay?.Invoke(collision);
                break;
            case EventType.Exit:
                isColliding = false;
                OnCollisionExit?.Invoke(collision);
                break;
            default: throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null);
        }
    }

    private bool MatchLayerMask(Component collision)
    {
        var layer = collision.gameObject.layer;
        return (layerMasks & (1 << layer)) == 1 << layer;
    }

    private enum EventType
    {
        Enter,
        Stay,
        Exit
    }
}