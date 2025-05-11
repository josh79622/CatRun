using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer1 : MonoBehaviour
{
    public Text countdownText;       // 設定要顯示的 UI Text（普通 UI Text）
    public Canvas guideline;
    public GameObject player;        // 玩家物件，先暫時禁用
    public Text Timer;
    public GameOverTrigger gameOverTrigger;
    public GoalTrigger goalTrigger;

    private float countdown = 3f;
    public float TimeLimit = 180.0f;
    private float starTime;
    private bool countdownFinished = false;

    public bool isCanMove;

    void Start()
    {
        if (isCanMove==false)
        {
            player.SetActive(false); // 禁用角色
            //CloseGuideline();
            Time.timeScale = 0f;
        }
        if (isCanMove == true)
        {
            CloseGuideline();
            Time.timeScale = 1f;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = Time.timeScale == 0 ? 1.0f : 0f;
        }

        if (Time.timeScale == 0)
        {
            OpenGuideline();
            Debug.Log("打开");
        }
        else
        {
            CloseGuideline();
            Debug.Log("关闭");
        }

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
                starTime = Time.time;
                player.SetActive(true); // 啟用角色
                Invoke("ClearText", 0.5f); // 0.5 秒後清除文字
            }
        }

        if (countdownFinished && !gameOverTrigger.isLost && !goalTrigger.isWon)
        {
            float usedTime = Time.time - starTime;
            float remainingTime = TimeLimit - usedTime;

            if (remainingTime <= 0)
            {
                gameOverTrigger.Lose();
            }
            else
            {
                int m = Mathf.FloorToInt(remainingTime / 60);
                int s = Mathf.FloorToInt(remainingTime) - m * 60;

                Timer.text = m.ToString("D2") + ":" + s.ToString("D2");
            }
        }

    }

    void CloseGuideline()
    {
        guideline.enabled = false;
    }

    void OpenGuideline()
    {
        guideline.enabled = true;
    }

    void ClearText()
    {
        countdownText.gameObject.SetActive(false);
    }
}