using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EatCoin : MonoBehaviour
{
    public Tilemap coinTilemap; // 把金幣的Tilemap拖進來
    public TileBase coinTile;
    public AudioClip collectSound;
    private AudioSource audioSource;

    private Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        eatCoin();
    }

    void eatCoin()
    {
        Bounds bounds = myCollider.bounds;

        Vector2 topLeft = new Vector2(bounds.min.x, bounds.max.y);
        Vector2 topCenter = new Vector2(bounds.center.x, bounds.max.y);
        Vector2 topRight = new Vector2(bounds.max.x, bounds.max.y);

        Vector2 middleLeft = new Vector2(bounds.min.x, bounds.center.y);
        Vector2 middleRight = new Vector2(bounds.max.x, bounds.center.y);

        Vector2 bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 bottomCenter = new Vector2(bounds.center.x, bounds.min.y);
        Vector2 bottomRight = new Vector2(bounds.max.x, bounds.min.y);


        Vector3 pos = transform.position;
        //Vector3 upPos = new Vector3(pos.x, pos.y + 2, pos.z);
        //Vector3 frontPos = new Vector3(pos.x + 2, pos.y, pos.z);
        //Vector3 downPos = new Vector3(pos.x, pos.y - 2, pos.z);
        Vector3[] positions = { pos, topLeft, topCenter, topRight, middleLeft, middleRight, bottomLeft, bottomCenter, bottomRight };
        for (var i = 0; i < 9; i++)
        {
            Vector3 p = positions[i];
            Vector3Int tilePosition = coinTilemap.WorldToCell(p);
            TileBase tile = coinTilemap.GetTile(tilePosition);

            if (tile == coinTile)
            {
                Debug.Log("PPP: " + p);
                coinTilemap.SetTile(tilePosition, null);

                if (audioSource != null && collectSound != null)
                {
                    audioSource.PlayOneShot(collectSound);
                }
                break;
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    Vector2 contactPoint = other.ClosestPoint(transform.position);

    //    Debug.Log("觸發接觸點：" + contactPoint);
    //    Debug.Log("transform.position：" + transform.position);

    //    Vector3Int tilePosition = coinTilemap.WorldToCell(new Vector3(contactPoint.x, contactPoint.y, transform.position.z));
    //    TileBase tile = coinTilemap.GetTile(tilePosition);

    //    if (tile == coinTile)
    //    {
    //        coinTilemap.SetTile(tilePosition, null);

    //        if (audioSource != null && collectSound != null)
    //        {
    //            audioSource.PlayOneShot(collectSound);
    //        }
    //    }
    //}
}
