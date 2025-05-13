using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Fish : MonoBehaviour
{
  

    public GameObject targetParent; // ����Ŀ��������ĸ�����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            bool activated = TryActivateOneChild(targetParent.transform);
            if (activated)
                Debug.Log("������һ��������");
            else
                Debug.Log("���������嶼�Ѽ�����账��");
        }
       
    }

    bool TryActivateOneChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (!child.gameObject.activeSelf)
            {
                GameManagerSewer.Instance.AddCount();
                child.gameObject.SetActive(true); // ֻ�����һ��δ�����
                return true;
            }
        }
        return false; // û�пɼ����
    }
}
