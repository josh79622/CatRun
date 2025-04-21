using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameOverTrigger : MonoBehaviour
{
    public Text gameOverText;
    public bool isActive = true;
    private bool hasLost = false;
    public Text instructionText;
    private string interfaceSceneName = "Interface";

    private void Update()
    {
        if (hasLost && Input.anyKeyDown)
        {
            SceneManager.LoadScene(interfaceSceneName);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "BadGuy" && isActive)
        {
            PoliceMovement policeMovement = collision.transform.GetComponent<PoliceMovement>();
            policeMovement.enabled = false;

            Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
            }

            Animator animator = collision.transform.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }

            gameOverText.gameObject.SetActive(true);

            StartCoroutine(WaitToEnableExit());
        }
    }

    private IEnumerator WaitToEnableExit()
    {
        yield return new WaitForSeconds(2f);
        hasLost = true;
        instructionText.text = "Press any key to return.";
    }
}
