using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinManagement : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        anim.SetTrigger("knock_over");
        transform.tag = "SlowDownBadGuy";
    }
}
