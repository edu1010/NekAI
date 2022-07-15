using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHud : MonoBehaviour
{
    [SerializeField]
    GameObject vida1;
    [SerializeField]
    GameObject vida2;
    [SerializeField]
    GameObject vida3;
    [SerializeField]
    GameObject escudo;
    [SerializeField]
    BasicDamageTaker healthSystem;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = healthSystem.CurrentHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentHealth = healthSystem.CurrentHealth;
        changeHud();
    }

    void changeHud()
    {
        switch (currentHealth)
        {
            case 1:
                vida1.SetActive(true);
                vida2.SetActive(false);
                vida3.SetActive(false);
                escudo.SetActive(false);
                break;
            case 2:
                vida1.SetActive(true);
                vida2.SetActive(true);
                vida3.SetActive(false);
                escudo.SetActive(false);
                break;
            case 3:
                vida1.SetActive(true);
                vida2.SetActive(true);
                vida3.SetActive(true);
                escudo.SetActive(false);
                break;
            case 4:
                vida1.SetActive(true);
                vida2.SetActive(true);
                vida3.SetActive(true);
                escudo.SetActive(true);
                break;
        }
            

    }
}
