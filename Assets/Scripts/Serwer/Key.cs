using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public AudioClip pickupSound; // ����Կ��ʰȡ��Ч
    private AudioSource audioSource;

    void Start()
    {
        // ȷ���� AudioSource ���
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false; // ���Զ�����
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManagerSewer.Instance.i++;

            // ����ʰȡ��Ч�����У�
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // �ӳ�����Կ�ף�����Ч�����꣨��ѡ��
            StartCoroutine(DisableAfterSound());
        }
    }

    IEnumerator DisableAfterSound()
    {
        // �ȴ���Ч������
        float delay = pickupSound != null ? pickupSound.length : 0f;
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }
}
