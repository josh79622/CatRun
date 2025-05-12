using UnityEngine;

public class Venom : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // 防止永远不销毁
    }

    private void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("毒液击中玩家！");
            GameManagerSewer.Instance.ReplaceCount(); // ✅ 模拟扣血
            Destroy(gameObject); // 撞到就消失
        }
    }
}
