using UnityEngine;

public class VenomSpawner : MonoBehaviour
{
    public GameObject venomPrefab; // ���붾ҺԤ����
    public float spawnInterval = 5f;

    private void Start()
    {
        InvokeRepeating("SpawnVenom", 1f, spawnInterval);
    }

    void SpawnVenom()
    {
        Instantiate(venomPrefab, transform.position, Quaternion.identity);
    }
}
