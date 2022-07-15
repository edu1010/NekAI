using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInEvent : MonoBehaviour
{
    public bool Event1 = false;
    public bool Event2=false;//You have a collider in the space of the event and if you enter bool is true, in exit bool = false
    PlayerHealthSystem hp;
    //Event1
    [SerializeField]
    EventController event1;
    [SerializeField]
    Animator event1Anim;
    [SerializeField]
    GameObject triggerEvent1;
    //Event2
    [SerializeField]
    GameObject door;
    [SerializeField]
    GameObject fall;
    [SerializeField]
    GameObject pieza;
    [SerializeField]
    GameObject Trigger;
    private void Awake()
    {
        hp = GetComponent<PlayerHealthSystem>();
    }
    private void OnEnable()
    {
        hp.OnPlayerDie += restEvent;
    }

    void restEvent()
    {
        if (Event1)
        {
            event1.StopEvent();
            event1Anim.Play("Puertas");
            triggerEvent1.SetActive(true);

        }
        if (Event2)
        {
            Destroy(GameObject.Find("BulletHell(Clone)"));
            pieza.SetActive(true);
            door.SetActive(false);
            Trigger.SetActive(true);
            fall.GetComponent<Animator>().Play("restart");
            eliminateBullets();
        }
        
    }
    void eliminateBullets()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("BulletsEvent");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);

        }
    }



}

enum Events
{
    Event1,
    event2
}
