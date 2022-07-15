using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAmInEvent : MonoBehaviour
{
    [SerializeField]
    WhatEventIam eventI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (eventI.Equals(WhatEventIam.event2))
            {
                collision.gameObject.GetComponent<DieInEvent>().Event2 = true;
            }
            else
            {
                collision.gameObject.GetComponent<DieInEvent>().Event1 = true;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (eventI.Equals(WhatEventIam.event2))
            {
                collision.gameObject.GetComponent<DieInEvent>().Event2 = false;
            }
            else
            {
                collision.gameObject.GetComponent<DieInEvent>().Event1 = false;
            }
        }
    }
}

enum WhatEventIam
{
    event1,
    event2
}