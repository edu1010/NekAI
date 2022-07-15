using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnToIntro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameFlowController.Instance.returnIntro();
    }
}
