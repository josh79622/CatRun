using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float translationSpeed = 4.0f;
    public float jumpForce = 5.0f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private int status = 0;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        status = 0;
        var horizonValue = Input.GetAxis("Horizontal");
        var speedUp = 1.0f;
        var isRunning = false;
        if (horizonValue != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            speedUp  = 2.0f;
        }
        

        float translation = horizonValue * translationSpeed * speedUp;
        //Debug.Log("translation:" + translation);
        if (translation != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(translation), 1, 1);
            status = 1;
        }
        rb.velocity = new Vector2(translation, rb.velocity.y);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //anim.SetTrigger("Jump");
            isGrounded = false;
        }
        if (!isGrounded)
        {

            status = 2;
            isRunning = false;
        }

        anim.SetInteger("Status", status);
        anim.SetBool("IsRunning", isRunning);

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("AttackTrigger");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision.contacts:" + collision.contacts[0].normal);
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            return;
        }
    }

    public bool GetIsGrounded ()
    {
        return isGrounded;
    }

    //void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log("collision.contacts:" + collision.contacts[0].normal);
    //    Debug.Log("G????");
    //        isGrounded = false;

    //}
}
