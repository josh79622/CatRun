using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 0, 100f); // ÿ����ת�Ƕ�

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
