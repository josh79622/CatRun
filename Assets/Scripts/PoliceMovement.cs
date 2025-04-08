using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    public GameObject target;
    public float speed;
    Animator anim;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(target.transform.position.x - transform.position.x) > 0.5)
        {
            anim.SetInteger("Status", 1);
            var direction = Mathf.Sign(target.transform.position.x - transform.position.x);
            Debug.Log("direction:" + direction);
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            transform.localScale = new Vector3(direction * transform.localScale.z, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            anim.SetInteger("Status", 0);
        }
        
    }
}
