using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkCat : ExtendedMonoBehaviour
{
    [SerializeField]
    Animator Text;
    bool finishAnimation = true;
    [SerializeField]
    Animator cat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Text.GetBool("Show"))
        {
            cat.Play("cat_Talking");
        }
        else
        {
            if (finishAnimation) SwitchAnimations();
            
        }
        void SwitchAnimations()
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    cat.Play("cat_idle");
                    finishAnimation = false;
                    InvokeLater(() => 
                    {
                        finishAnimation = true;
                    },4f);                    
                    break;
                case 1:
                    cat.Play("cat_blink");
                    finishAnimation = false;
                    InvokeLater(() =>
                    {
                        finishAnimation = true;
                    }, 1f);
                    break;
                case 2:
                    cat.Play("cat_glitch");
                    finishAnimation = false;
                    InvokeLater(() =>
                    {
                        finishAnimation = true;
                    }, 0.5f);                
                    break;
                case 3:
                    cat.Play("cat_movingEars");
                    finishAnimation = false;
                    InvokeLater(() =>
                    {
                        finishAnimation = true;
                    }, 2f);
                    break;
            }
        }
    }
}
