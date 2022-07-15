using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutoHealth : MonoBehaviour
{
    public Conversation DialogueData;
    void OnEnable()
    {
        BasicDamageTaker.OnDieS += ShowTextTuto;
    }

    void ShowTextTuto() 
    {
        StartDialegue();
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartDialegue()
    {
        DialogueManager.Instance.StartConversation(DialogueData, gameObject);
    }


}
