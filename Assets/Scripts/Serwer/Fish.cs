using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Fish : MonoBehaviour
{
  

    public GameObject targetParent; // 包含目标子物体的父对象

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            bool activated = TryActivateOneChild(targetParent.transform);
            if (activated)
                Debug.Log("激活了一个子物体");
            else
                Debug.Log("所有子物体都已激活，无需处理");
        }
       
    }

    bool TryActivateOneChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (!child.gameObject.activeSelf)
            {
                GameManagerSewer.Instance.AddCount();
                child.gameObject.SetActive(true); // 只激活第一个未激活的
                return true;
            }
        }
        return false; // 没有可激活的
    }
}
