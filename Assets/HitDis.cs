using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDis : MonoBehaviour
{

    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("HideAfterDelay", 5f);
        //StartCoroutine(HideAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator HideAfterDelay()
    //{
    //    yield return new WaitForSeconds(2f); // �ȴ�10�� 
    //    StopCoroutine(HideAfterDelay());
    //    gameObject.SetActive(false); // �������� 
    //}
    public void HideAfterDelay()
    {
        gameObject.SetActive(false); // �������� 
    }

}
