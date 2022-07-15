using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFirstCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameFlowController.Instance.PasarLVL();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
