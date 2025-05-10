using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerSewer : MonoBehaviour
{
    public static GameManagerSewer Instance { get; private set; }

    public int SceneCount = 2;

    public GameObject GameOverText;


    public  int i = 0;
    public GameObject goDoorOne;

    public Text keyText;    

    private void Awake()
    {
        // ����Ѿ���ʵ�����Ҳ����Լ��������Լ�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ������Ϊ��ǰʵ��������ѡ������
        Instance = this;
      
    }

    public void AddCount()
    {
        if (SceneCount <3)
        {
            SceneCount++;
        }
    }

    public void ReplaceCount()
    {
        if (SceneCount >0)
        {
            SceneCount--;
        }
    }

    // ʾ������
    public void DoSomething()
    {
        Debug.Log("GameManagerSewer ��������ĳ���߼�...");
    }

    private void Update()
    {
        if (SceneCount<=0)
        {
            GameOverText.SetActive(true);
            StartCoroutine(enumerator());
        }
        if (i>=3)
        {
            goDoorOne.SetActive(false);
        }
        keyText.text = "KEY:" + i+ " / 3";
    }




    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(3);
        GameOverText.SetActive(false);
        SceneManager.LoadScene("sewer");
        
    }
}

