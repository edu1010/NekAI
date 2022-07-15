using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothSimple : MonoBehaviour
{
    public Transform Player;
    Vector3 offset;
    public float smooth =0.0125f;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        followSmooth();
    }
    void followSmooth()
    {
        transform.position = Vector3.Lerp(transform.position, Player.position + offset, smooth);
    }
}
