using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinishBulletHellEvent : MonoBehaviour
{
    [SerializeField]
    AnimationEventController animation;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            animation.StartAnimation();
            Destroy(gameObject);

        }
    }
}
