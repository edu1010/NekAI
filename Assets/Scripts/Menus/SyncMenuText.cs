using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncMenuText : MonoBehaviour
{
    public CanvasGroup Conf;
     MenuPanel text;
    CanvasGroup textCanvas;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<MenuPanel>();
        textCanvas = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Conf.alpha == 1 && textCanvas.alpha!=0)
        {

            text.Hide();
        }
        else
        {
            if (Conf.alpha == 0 && textCanvas.alpha != 1)
            {
                text.Show();
            }
        }
    }
}
