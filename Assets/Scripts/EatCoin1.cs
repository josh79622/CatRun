using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class EatCoin1 : MonoBehaviour
{
    public Tilemap coinTilemap; // 把金幣的Tilemap拖進來
    public TileBase coinTile;
    public AudioClip collectSound;
    public Text coinCounter; 
    private AudioSource audioSource;

    private Collider2D myCollider;
    private CatMovement1 catMovement;

    public int remainingCoins = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myCollider = GetComponent<Collider2D>();
        catMovement = GetComponent<CatMovement1>();

        //remainingCoins = CountRemainingCoins();

    }

    // Update is called once per frame
    void Update()
    {

        if (coinCounter)
        {
            coinCounter.text = "coins："+remainingCoins;
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Coin on trigger");
        if (!catMovement.isDead)
        {
            Debug.Log("cat is not dead");
            Vector2 contactPoint = other.ClosestPoint(transform.position);

            Debug.Log("觸發接觸點：" + contactPoint);
            Debug.Log("transform.position：" + transform.position);

            Vector3Int tilePosition = coinTilemap.WorldToCell(new Vector3(contactPoint.x, contactPoint.y, transform.position.z));
            TileBase tile = coinTilemap.GetTile(tilePosition);

            if (tile == coinTile)
            {
                coinTilemap.SetTile(tilePosition, null);

                if (audioSource != null && collectSound != null)
                {
                    audioSource.PlayOneShot(collectSound);
                }

                remainingCoins += 1;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Coin on trigger");
        if (!catMovement.isDead)
        {
            Debug.Log("cat is not dead");
            Vector2 contactPoint = other.ClosestPoint(transform.position);

            Debug.Log("觸發接觸點：" + contactPoint);
            Debug.Log("transform.position：" + transform.position);

            Vector3Int tilePosition = coinTilemap.WorldToCell(new Vector3(contactPoint.x, contactPoint.y, transform.position.z));
            TileBase tile = coinTilemap.GetTile(tilePosition);

            if (tile == coinTile)
            {
                coinTilemap.SetTile(tilePosition, null);

                if (audioSource != null && collectSound != null)
                {
                    audioSource.PlayOneShot(collectSound);
                }

                remainingCoins += 1;
            }
        }
    }

    int CountRemainingCoins()
    {
        int count = 0;

        // 取得 tilemap 的有效範圍
        BoundsInt bounds = coinTilemap.cellBounds;

        // 遍歷範圍內所有格子
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = coinTilemap.GetTile(pos);
            if (tile == coinTile)
            {
                count++;
            }
        }

        return count;
    }
}
