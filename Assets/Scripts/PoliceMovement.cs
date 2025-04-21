using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMovement : MonoBehaviour
{
    public GameObject target;
    public float speed = 6;
    public float climbingSpeed = 3.5f;
    private float speedUp = 1.0f;
    Animator anim;

    private Rigidbody2D rb;
    private bool isClimbing = false;
    private bool hasAWall = false;
    private Bounds wallBounds;
    private int Status = 0;

    private DropControl dropControl;

    private Collider2D collidedWall;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dropControl = GetComponent<DropControl>();
    }

    private void Update()
    {
        Bounds myBounds = GetComponent<Collider2D>().bounds;
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

        if (isClimbing)
        {
            Status = 2;
            rb.velocity = new Vector2(0, climbingSpeed);
        }
        else
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
            } else
            {
                Status = 0;
            }
        }
        anim.SetInteger("Status", Status);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isClimbing)
        {
            Bounds myBounds = GetComponent<Collider2D>().bounds;
            Bounds bounds = collision.collider.bounds;
            if (bounds.Contains(myBounds.center) && collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("!!");
        if (other.CompareTag("SlowDownBadGuy"))
        {
            Debug.Log("SlowDown!!!");
            speedUp = 0.3f;
            StartCoroutine(WaitToBackToNormalSpeed());
        }
    }

    private IEnumerator WaitToBackToNormalSpeed()
    {
        yield return new WaitForSeconds(2f);
        speedUp = 1f;
    }
}
