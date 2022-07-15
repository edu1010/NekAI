using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : ExtendedMonoBehaviour
{
    Animator animator;
    
    CameraShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraShake = GetComponent<CameraShake>();
    }

    public void StartAnimation()
    {
        InvokeLater(()=>cameraShake.shake(),1);
        animator.SetBool("Open", true);
    }
    public void EndAnimation()
    {
        gameObject.SetActive(false);
        animator.SetBool("Open", false);
    }
}
