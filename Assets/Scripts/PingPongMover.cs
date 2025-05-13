using UnityEngine;

public class PingPongMover : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 target;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            enabled = false;
            return;
        }

        target = pointB.position;
    }

    void Update()
    {
        // 移動到目標位置
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // 根據移動方向翻轉 scale.x
        if (target.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);  // 向左
        }
        else if (target.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);   // 向右
        }

        // 到達目標點時切換目標
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }
}