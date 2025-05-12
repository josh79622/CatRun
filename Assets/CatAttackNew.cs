using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttackNew : MonoBehaviour
{
    public GameObject target;

    public Transform attackPoint;
    public Vector2 attackSize = new Vector2(0.5f, 0.5f);
    public LayerMask enemyLayers;

    private Animator anim;
    bool isnew;
    public GameObject[] transHit;
    public GameObject[] policeHit; 

    private void Start()
    {
        target = target == null ? GameObject.FindGameObjectWithTag("Player") : target;
        anim = GetComponent<Animator>();
    }
    public void Attack()
    {

        Debug.Log("ATTACK!");
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position, attackSize, 0, enemyLayers);
        Debug.Log("HITS: " + hits);
        foreach (Collider2D hit in hits)
        {
            Debug.Log("ÃüÖÐ£º" + hit.name);
            Debug.Log("target£º" + target.name);
            if (hit.name=="bin1")
            {
                gameObject.layer = LayerMask.NameToLayer("catGhost");
                Invoke("reLife", 5f);
                target = hit.gameObject;
                transHit[0].SetActive(true);
                for (int i = 0; i < policeHit.Length; i++)
                {
                    if (policeHit[i] != null)
                    {
                        policeHit[i].SetActive(true);
                    }
                }
            }
            //
            if (hit.name == "bin2")
            {
                gameObject.layer = LayerMask.NameToLayer("catGhost");
                Invoke("reLife", 5f);
                target = hit.gameObject;
                transHit[1].SetActive(true);
                for (int i = 0; i < policeHit.Length; i++)
                {
                    if (policeHit[i] != null)
                    {
                        policeHit[i].SetActive(true);
                    }
                }
            }
            //
            if (hit.name == "bin3")
            {
                gameObject.layer = LayerMask.NameToLayer("catGhost");
                Invoke("reLife", 5f);
                target = hit.gameObject;
                transHit[2].SetActive(true);
                for (int i = 0; i < policeHit.Length; i++)
                {
                    if (policeHit[i] != null)
                    {
                        policeHit[i].SetActive(true);
                    }
                }
            }
            //
            if (hit.name == "bin4")
            {
                gameObject.layer = LayerMask.NameToLayer("catGhost");
                Invoke("reLife", 5f);
                target = hit.gameObject;
                transHit[3].SetActive(true);
                for (int i = 0; i < policeHit.Length; i++)
                {
                    if (policeHit[i] != null)
                    {
                        policeHit[i].SetActive(true);
                    }
                }
            }
            //
            if (hit.name == "bin5")
            {
                gameObject.layer = LayerMask.NameToLayer("catGhost");
                Invoke("reLife", 5f);
                target = hit.gameObject;
                transHit[4].SetActive(true);
                for (int i = 0; i < policeHit.Length; i++)
                {
                    if (policeHit[i] != null)
                    {
                        policeHit[i].SetActive(true);
                    }
                }
            }
            //
            if (hit.name == "bin6")
            {
                gameObject.layer = LayerMask.NameToLayer("catGhost");
                Invoke("reLife", 5f);
                target = hit.gameObject;
                transHit[5].SetActive(true);
                for (int i = 0; i < policeHit.Length; i++)
                {
                    if (policeHit[i] != null)
                    {
                        policeHit[i].SetActive(true);
                    }
                }
            }
            //
            if (hit.name == "bin7")
            {
                gameObject.layer = LayerMask.NameToLayer("catGhost");
                Invoke("reLife", 5f);
                target = hit.gameObject;
                transHit[6].SetActive(true);
                for (int i = 0; i < policeHit.Length; i++)
                {
                    if (policeHit[i] != null)
                    {
                        policeHit[i].SetActive(true);
                    }
                }
            }

        }
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("AttackTrigger");
            Attack();
        }
    }
    public void reLife()
    {
        gameObject.layer = LayerMask.NameToLayer("cat");
        target =  GameObject.FindGameObjectWithTag("Player");
    }


}
