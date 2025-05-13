using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMainMenu : MonoBehaviour
{
    public string mainMenuSceneName = "Interface"; // �ĳ�������˵�������

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("������ڣ��������˵�");
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
