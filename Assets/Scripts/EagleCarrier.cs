using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleCarrier : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 貓咪站上來了，就讓牠成為老鷹的子物件
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 貓咪離開了，解除關係
            collision.transform.SetParent(null);
        }
    }
}