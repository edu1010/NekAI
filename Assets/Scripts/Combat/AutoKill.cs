using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoKill : MonoBehaviour
{
    [SerializeField] private CollisionDetector2D collisionDetector2D;

    private void OnEnable()
    {
        collisionDetector2D.OnCollisionEnter += KillPlayer;
    }

    private void OnDisable()
    {
        collisionDetector2D.OnCollisionEnter -= KillPlayer;
    }

    private void KillPlayer(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<IDamageTaker>();
        player?.Die();
    }
}
