using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMainMenu : MonoBehaviour
{
    public string mainMenuSceneName = "Interface"; // 改成你的主菜单场景名

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("到达出口，返回主菜单");
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
