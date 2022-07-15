using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCamera : MonoBehaviour
{
    [SerializeField]
    GameObject camara;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            camara.GetComponent<CameraManager>().enabled = false;
        }
    }
}
