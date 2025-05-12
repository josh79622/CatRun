using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip pickupSound; // 拖入钥匙拾取音效
    private AudioSource audioSource;

    void Start()
    {
        // 确保有 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false; // 不自动播放
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManagerSewer.Instance.i++;

            // 播放拾取音效（如有）
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // 延迟隐藏钥匙，让音效播放完（可选）
            StartCoroutine(DisableAfterSound());
        }
    }

    IEnumerator DisableAfterSound()
    {
        // 等待音效播放完
        float delay = pickupSound != null ? pickupSound.length : 0f;
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }
}
