using UnityEngine;
using UnityEngine.UI;

public class SimpleCountdown : MonoBehaviour
{
    public Text countdownText;       // 設定要顯示的 UI Text（普通 UI Text）
    public GameObject player;        // 玩家物件，先暫時禁用

    private float countdown = 3f;
    private bool countdownFinished = false;

    void Start()
    {
        player.SetActive(false); // 禁用角色
    }

    void Update()
    {
        if (!countdownFinished)
        {
            countdown -= Time.deltaTime;

            if (countdown > 0)
            {
                countdownText.text = Mathf.Ceil(countdown).ToString();
            }
            else
            {
                countdownFinished = true;
                countdownText.text = "GO!";
                player.SetActive(true); // 啟用角色
                Invoke("ClearText", 0.5f); // 0.5 秒後清除文字
            }
        }
    }

    void ClearText()
    {
        countdownText.gameObject.SetActive(false);
    }
}