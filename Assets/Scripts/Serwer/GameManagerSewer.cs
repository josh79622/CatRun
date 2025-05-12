using UnityEngine.SceneManagement;
using UnityEngine;

using System.Collections;

using UnityEngine.UI;

public class GameManagerSewer : MonoBehaviour
{
    public static GameManagerSewer Instance { get; private set; }

    public int SceneCount = 2;

    public GameObject GameOverText;


    public GameObject GameMusic;

    public int i = 0;
    public GameObject goDoorOne;
 


    public Text keyText;

    private void Awake()
    {
        // 如果已经有实例并且不是自己，销毁自己
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 否则设为当前实例，并可选择不销毁
        Instance = this;

    }

    public void AddCount()
    {
        if (SceneCount < 3)
        {
            SceneCount++;
        }
    }

    public void ReplaceCount()
    {
        if (SceneCount > 0)
        {
            SceneCount--;
        }
    }

    // 示例方法
    public void DoSomething()
    {
        Debug.Log("GameManagerSewer 正在运行某个逻辑...");
    }

    private void Update()
    {
        if (SceneCount <= 0)
        {
            GameOverText.SetActive(true);
            StartCoroutine(enumerator());
        }
        if (i >= 3)
        {
            goDoorOne.SetActive(false);
            GameMusic.gameObject.SetActive(true);
        }
        keyText.text = "KEY:" + i + " / 3";
    }




    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(3);
        GameOverText.SetActive(false);
        SceneManager.LoadScene("sewer");

    }
}