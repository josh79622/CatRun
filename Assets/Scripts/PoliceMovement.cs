using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{

    private GameObject target;
    public float speed = 6;
    public float climbingSpeed = 3.5f;
    private float speedUp = 1.0f;
    Animator anim;

    private float jumpForce = 15.0f;
    private bool isGrounded;

    private Rigidbody2D rb;
    private bool isClimbing = false;
    private bool hasAWall = false;
    private Bounds wallBounds;
    private int Status = 0;
    private bool isGoingAfter = false;

    private DropControl dropControl;

    private Collider2D collidedWall;

    private CatMovement catMovement;
    private Transform confused;

    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        target = target == null ? GameObject.FindGameObjectWithTag("Player") : target;
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        dropControl = this.GetComponent<DropControl>();
        catMovement = target.GetComponent<CatMovement>();
        confused = transform.parent.Find("Confused");
        confused.gameObject.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 处理闪烁效果
        if (speedUp < 1)
        {
            StartFlash();
        } else
        {
            StopFlash();
        }
        // 避免与其他警察重叠
        AvoidOtherPolice();
        // 判断是否开始追踪玩家
        if (!isGoingAfter)
        {
            Vector2 diff = target.transform.position - transform.position;
            float distance = diff.magnitude;
            if (distance < 40)
            {
                isGoingAfter = true;
            }
        }
        // 主要行为逻辑
        if (isGoingAfter)
        {
            if (catMovement.isHiding)
            {
                // 玩家隐藏时的行为
                confused.gameObject.SetActive(true);

                rb.velocity = new Vector2(0, 0);
                anim.SetInteger("Status", 0);
            }
            else
            {
                confused.gameObject.SetActive(false);
                Bounds myBounds = this.GetComponent<Collider2D>().bounds;

                if (collidedWall)
                //Debug.Log("isOnTheWall(target, collidedWall): " + isOnTheWall(target, collidedWall));
                 // 墙检测逻辑
                if (collidedWall && isOnTheWall(target, collidedWall))
                {
                    if (myBounds.min.y < collidedWall.bounds.max.y)
                    {
                        isClimbing = true;
                    }
                    else
                    {
                        isClimbing = false;
                    }
                }
                else
                {
                    isClimbing = false;
                }
                // 攀爬行为
                if (isClimbing)
                {
                    Status = 2;
                    rb.velocity = new Vector2(0, climbingSpeed);
                }
                // 地面行为
                else if (isGrounded)
                {
                    if (transform.position.y - target.transform.position.y > 2)
                    {
                        dropControl.Drop();
                    }
                    if (Mathf.Abs(target.transform.position.x - transform.position.x) > 1)
                    {
                        Status = 1;
                        var direction = Mathf.Sign(target.transform.position.x - transform.position.x);
                        rb.velocity = new Vector2(direction * speed * speedUp, rb.velocity.y);
                        transform.localScale = new Vector3(direction * transform.localScale.z, transform.localScale.y, transform.localScale.z);
                    }
                    else
                    {
                        Status = 0;
                    }
                }
                // 跳跃行为
                Vector2 pos = transform.position;
                Collider2D obstacle = Physics2D.OverlapPoint(pos, LayerMask.GetMask("blockWalls"));

                if (obstacle != null &&
                    target.transform.position.y > transform.position.y &&
                    Mathf.Abs(target.transform.position.x - transform.position.x) < 2 &&
                    isGrounded
                    )
                {
                    Status = 3;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    isGrounded = false;
                }

                anim.SetInteger("Status", Status);

                
            }
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isClimbing)
        {
            Bounds myBounds = this.GetComponent<Collider2D>().bounds;
            Bounds bounds = collision.collider.bounds;

            Vector3 centerPos = new Vector3(myBounds.center.x, myBounds.center.y, 0);
            if (bounds.Contains(centerPos) && collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
            {
                //Debug.Log("YYY " + collision.collider.name);
                collidedWall = collision.collider;
            }
            else
            {
                //collidedWall = null;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == collidedWall)
        {
            collidedWall = null;
        }
    }
    // 墙检测辅助方法
    private bool isOnTheWall(GameObject target, Collider2D wall)
    {
        Transform tra = target.transform;
        Vector3 position = tra.position;
        Bounds wallBounds = wall.bounds;
        if (position.x >= wallBounds.min.x && position.x <= wallBounds.max.x && position.y >= wallBounds.max.y)
        {
            return true;
        }
        return false;
    }
    // 跳跃方法
    public void SpeedJump()
    {
        rb.velocity = new Vector2(rb.velocity.x * 2, jumpForce);
        isGrounded = false;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("SlowDownBadGuy"))
        {
            speedUp = 0.3f;
            Debug.Log("0.3");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "JumpTrigger")
        {
            SpeedJump();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(WaitToBackToNormalSpeed());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            
            StartCoroutine(WaitToGround());
        }
    }

    private IEnumerator WaitToGround()
    {
        yield return new WaitForSeconds(0.2f);
        isGrounded = true;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    private IEnumerator WaitToBackToNormalSpeed()
    {
        yield return new WaitForSeconds(1f);
        speedUp = 1f;
    }
    // 避免重叠方法
    void AvoidOtherPolice()
    {
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, 1f, LayerMask.GetMask("police"));

        foreach (Collider2D other in nearby)
        {
            if (other.gameObject != this.gameObject)
            {
                Vector2 away = (Vector2)(transform.position - other.transform.position);
                transform.position += (Vector3)(away.normalized * 0.02f); 
            }
        }
    }

    //闪烁效果方法
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
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.2f);

            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
