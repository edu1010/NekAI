using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public virtual void Fire()
    {

    }
    public virtual bool ShouldShoot()
    {
        return false;
    }
}
