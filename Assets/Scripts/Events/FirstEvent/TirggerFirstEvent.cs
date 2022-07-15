using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirggerFirstEvent : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    public delegate void StartEventDelegate();
    public static StartEventDelegate start;
    [SerializeField]
    GameObject laser;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            animator.Play("StartCombat");
            start?.Invoke();
            // animator.SetBool("Open", true);
            laser.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
