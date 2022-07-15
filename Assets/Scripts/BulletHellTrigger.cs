using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellTrigger : MonoBehaviour
{
   [SerializeField]
    GameObject Enemie;
    [SerializeField]
    GameObject Door;
    [SerializeField]
    AnimationEventController animation;
    [SerializeField]
    Transform spawnPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            var a =Instantiate(Enemie, new Vector3(spawnPos.position.x, spawnPos.position.y, 0), Quaternion.identity);
            a.transform.position = spawnPos.position;
            Enemie.SetActive(true);
            Door.SetActive(true);
            transform.gameObject.SetActive(false);

        }
        
    }
}
