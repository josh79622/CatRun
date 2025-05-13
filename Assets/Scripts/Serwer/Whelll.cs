using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Whelll : MonoBehaviour
{
    public GameObject targetParent; // ����Ŀ��������ĸ�����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool deactivated = TryDeactivateOneActiveChild(targetParent.transform);
            if (deactivated)
                Debug.Log("ʧ����һ��������");
            else
                Debug.Log("���������嶼��ʧ����账��");
        }
    }

    bool TryDeactivateOneActiveChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false); // ֻʧ���һ�������������
                GameManagerSewer.Instance.ReplaceCount();
                return true;
            }
        }
        return false; // ���������嶼��ʧ��
    }
}
