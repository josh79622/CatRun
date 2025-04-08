using UnityEngine;

public class CatPlatformDrop : MonoBehaviour
{
    public float dropDuration = 0.3f; // 多久後恢復碰撞
    public LayerMask platformLayer;   // 指定平台圖層（Props 或 Platform）
    public Transform groundCheck;
    public float checkRadius = 0.2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Collider2D platform = Physics2D.OverlapCircle(groundCheck.position, checkRadius, platformLayer);
            if (platform != null)
            {
                StartCoroutine(TemporarilyDisableCollision(platform));
            }
        }
    }

    private System.Collections.IEnumerator TemporarilyDisableCollision(Collider2D platform)
    {
        Collider2D myCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(myCollider, platform, true);
        yield return new WaitForSeconds(dropDuration);
        Physics2D.IgnoreCollision(myCollider, platform, false);
    }
}