using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;


public class GameOverTrigger1: MonoBehaviour
{
    public Text gameOverText;
    public bool isActive = true;
    public bool isLost { get; private set; } = false;
    public bool hasLost { get; private set; } = false;
    public Image[] hearts;
    public Text instructionText;
    private string interfaceSceneName = "Interface";
    private int lifeNumber;
    private CatMovement1 catMovement;

    private void Start()
    {
        lifeNumber = hearts.Length;
        catMovement = transform.GetComponent<CatMovement1>();
    }
    private void Update()
    {
        if (hasLost && Input.anyKeyDown)
        {
            SceneManager.LoadScene(interfaceSceneName);
            Debug.Log("Ìø×ªÁË");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "BadGuy")
        {
            if (!catMovement.isDead)
            {
                if (lifeNumber > 0)
                {
                    Image heart = hearts[lifeNumber - 1];
                    heart.color = new Color(0, 0, 0, 0.5f);
                    catMovement.Die();
                    lifeNumber -= 1;
                }
                else
                {
                    Lose();
                }
            }
        }
    }
    public void Lose()
    {
        if (isActive)
        {
            isLost = true;

            GameObject[] policeList = GameObject.FindGameObjectsWithTag("BadGuy");
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] stopList = policeList.Concat(players).ToArray<GameObject>();

            foreach (GameObject obj in stopList)
            {
                MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts)
                {
                    if(script.GetType().Name != "GameOverTrigger1")
                    {
                        script.enabled = false;
                    }
                }

                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true;
                }

                Animator animator = obj.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.enabled = false;
                }
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
