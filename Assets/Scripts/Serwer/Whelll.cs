using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Whelll : MonoBehaviour
{
    public GameObject targetParent; // 包含目标子物体的父对象

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool deactivated = TryDeactivateOneActiveChild(targetParent.transform);
            if (deactivated)
                Debug.Log("失活了一个子物体");
            else
                Debug.Log("所有子物体都已失活，无需处理");
        }
    }

    bool TryDeactivateOneActiveChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false); // 只失活第一个激活的子物体
                GameManagerSewer.Instance.ReplaceCount();
                return true;
            }
        }
        return false; // 所有子物体都已失活
    }
}
