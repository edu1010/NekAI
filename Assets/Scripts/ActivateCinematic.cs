using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivateCinematic : MonoBehaviour
{
    [SerializeField]
    PlayableDirector playableDirector;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playableDirector.Play();
            gameObject.SetActive(false);
        }
        
    }
}
