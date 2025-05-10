using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceShowUpTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject policePrefab;        // 警察預製體（拖進 Inspector）
    public Transform spawnPoint;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CallPolice()
    {
        Instantiate(policePrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("警察出現了！");
    }
}
