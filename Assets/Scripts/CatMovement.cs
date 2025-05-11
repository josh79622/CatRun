using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CatMovement : MonoBehaviour
{
    public EnergyBar energy;
    public float energyCost = 1f;
    public float energyGain = 20f;
    public float translationSpeed = 4.0f;
    public float jumpForce = 5.0f;
    public bool isHiding { get; private set; } = false;


    public bool isDead { get; private set; } = false;
    private bool isGrounded;
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
            speedUp = 0.6f;
            energy.GainEnergy(1f);
        }
        else if (horizonValue != 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && energy.currentEnergy >= energyCost)
        {
            isRunning = true;
            speedUp  = 2.0f;
            energy.UseEnergy(energyCost);
            
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

        if (status == 0)
        {
            energy.GainEnergy(0.02f);
        }


        anim.SetInteger("Status", isDead ? -1 : status);
        if (!isDead)
        {
            anim.SetBool("IsRunning", isRunning);

            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("AttackTrigger");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision.contacts:" + collision.contacts[0].normal);
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            //return;
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            energy.GainEnergy(energyGain);
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

            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
