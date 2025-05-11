using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CatMovement : MonoBehaviour
{
    public EnergyBar energy;
    public float energyCost = 1f;
    public float energyGain = 20f;
    public float translationSpeed = 4.0f;
    public float normalGravity = 3f;
    public float waterGravity = 1f;
    public float jumpForce = 5.0f;
    private float originalJumpForce;
    public bool isHiding { get; private set; } = false;


    public bool isDead { get; private set; } = false;
    private bool isUnderWater = false;
    private bool isGrounded;
    public bool isAllowDoubleJump = false;
    private bool canDoubleJump = false;
    private Rigidbody2D rb;
    private int status = 0;
    Animator anim;

    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        status = 0;
        var horizonValue = Input.GetAxis("Horizontal");
        var speedUp = 1.0f;
        var isRunning = false;
        if (isDead)
        {
            status = -1;
            isRunning = false;
            speedUp = 0.6f;
            energy.GainEnergy(1f);
        }
        else if (horizonValue != 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && energy.currentEnergy >= energyCost)
        {
            isRunning = true;
            speedUp  = 2.0f;
            energy.UseEnergy(energyCost);
            
        }

        if (isUnderWater)
        {
            speedUp = 0.5f;
        }
        

        float translation = horizonValue * translationSpeed * speedUp;
        //Debug.Log("translation:" + translation);
        if (translation != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(translation), 1, 1);
            status = 1;
        }
        rb.velocity = new Vector2(translation, rb.velocity.y);



        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || isUnderWater))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isGrounded = false;
                if (isAllowDoubleJump)
                    canDoubleJump = true;
            }
            else if (isAllowDoubleJump && canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
        if (!isGrounded)
        {

            status = 2;
            isRunning = false;
        }

        if (status == 0)
        {
            energy.GainEnergy(0.02f);
        }


        anim.SetInteger("Status", isDead ? -1 : status);
        anim.SetBool("IsRunning", isRunning);
        if (!isDead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("AttackTrigger");
            }
        }

        //Debug.Log("isGrounded: " + isGrounded);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ////Debug.Log("collision.contacts:" + collision.contacts[0].normal);
        //if (collision.contacts[0].normal.y > 0.5f)
        //{
        //    isGrounded = true;
        //    //return;
        //}
        Debug.Log("Collision!");
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;

            // 如果法線向上，代表貓咪在對方上面（踩在上面）
            if (normal.y > 0.5f)
            {
                isGrounded = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isGrounded)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal = contact.normal;

                // 貓咪可以踢牆跳
                if (Mathf.Abs(normal.x) > 0.9f)
                {
                    isGrounded = true;
                    //return;
                }
            }
        }
        
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple") && !isDead)
        {
            Destroy(other.gameObject);
            energy.GainEnergy(energyGain);
        }

        if (other.CompareTag("extraLife") && !isDead)
        {
            Destroy(other.gameObject);
            GameOverTrigger got = GetComponent<GameOverTrigger>();
            got.AddHeart();
        }

        if (other.gameObject.CompareTag("Water"))
        {
            Debug.Log("進入水中");

            isUnderWater = true;

            rb.drag = 100f;
            rb.gravityScale = waterGravity;
            jumpForce = originalJumpForce * 0.2f;
            // 可以改變移動速度、動畫等等
        }

        if (other.gameObject.CompareTag("fallingWater"))
        {
            rb.gravityScale = normalGravity * 1.5f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water") && rb.drag > 1f)
        {
            rb.drag = 0.8f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Debug.Log("離開水中");

            isUnderWater = false;

            rb.drag = 0;
            rb.gravityScale = normalGravity;
            jumpForce = originalJumpForce;
        }
        if (other.gameObject.CompareTag("fallingWater"))
        {
            rb.gravityScale = waterGravity;
        }
    }

    public bool GetIsGrounded ()
    {
        return isGrounded;
    }

    public void setHidden (bool value)
    {
        isHiding = value;
    }

    public void Die ()
    {
        anim.SetTrigger("IsDead");
        isDead = true;
        setHidden(true);
        gameObject.layer = LayerMask.NameToLayer("catGhost");
        Invoke("reLife", 5f);
        Invoke("StartFlash", 3f);
    }

    public void reLife ()
    {
        StopFlash();
        isDead = false;
        setHidden(false);
        gameObject.layer = LayerMask.NameToLayer("cat");
    }

    public void StartFlash()
    {
        if (flashCoroutine == null)
        {
            flashCoroutine = StartCoroutine(FlashRoutine());
        }
    }

    public void StopFlash()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
            flashCoroutine = null;

            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private IEnumerator FlashRoutine()
    {
        while (true)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.1f);
            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = new Color(1f, 1f, 1f, 0.4f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
