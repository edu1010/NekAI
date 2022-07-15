using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBullet : MonoBehaviour
{
    GameObject TriggerAnimation;
    // Start is called before the first frame update
    void Start()
    {
        TriggerAnimation = GameObject.Find("TriggerFinishBulletHellEvent");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerAnimation.GetComponent<Collider2D>().enabled = true;
    }
}
