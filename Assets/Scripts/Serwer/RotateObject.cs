using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 0, 100f); // Ã¿ÃëÐý×ª½Ç¶È

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
