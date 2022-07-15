using System;
using UnityEngine;

public class StompEffect : ExtendedMonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private float duration;
    [SerializeField] private new Collider2D collider2D;

    public void DoStompEffect()
    {
        particles.Play();
        collider2D.enabled = true;
        InvokeLater(() =>
        {
            particles.Stop();
            collider2D.enabled = false;
        }, duration);
    }
}