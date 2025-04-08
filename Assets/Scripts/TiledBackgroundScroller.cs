using UnityEngine;

public class TiledBackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.02f;
    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset.x += scrollSpeed * Time.deltaTime;
        rend.material.mainTextureOffset = offset;
    }
}