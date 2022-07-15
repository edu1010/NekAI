using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFirstShadow : MonoBehaviour
{
    [SerializeField]
    GameObject shadow;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            shadow.SetActive(true);
        }
    }
}
