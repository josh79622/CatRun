using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinManagement : MonoBehaviour
{
    private Animator anim;
    public bool isKnocked { get; private set; } = false;
    
    private PoliceShowUpTrigger policeShowUpTrigger;
    public GameObject stopWall;

    public GameObject hiddenApple;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        policeShowUpTrigger = GetComponent<PoliceShowUpTrigger>();
        hiddenApple.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        if (policeShowUpTrigger)
        {
            Debug.Log("CALL POLICE!");
            if (!isKnocked)
            {
                policeShowUpTrigger.CallPolice();

            }
        }
        if (stopWall)
        {
            stopWall.SetActive(false);
        }
        if (hiddenApple)
        {
            Invoke("showHiddenApple", 1.15f);
        }

        anim.SetTrigger("knock_over");
        transform.tag = "SlowDownBadGuy";
        isKnocked = true;
    }

    void showHiddenApple ()
    {
        hiddenApple.SetActive(true);
    }
}
