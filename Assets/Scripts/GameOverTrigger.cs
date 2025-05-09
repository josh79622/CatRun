using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GameOverTrigger : MonoBehaviour
{
    public Text gameOverText;
    public bool isActive = true;
    public bool isLost { get; private set; } = false;
    public bool hasLost { get; private set; } = false;
    //public Image[] hearts;
    public List<Image> hearts = new List<Image>();
    public int initialLifeNumber = 3;
    public Text instructionText;
    private string interfaceSceneName = "Interface";
    private int lifeNumber;
    private CatMovement catMovement;
    private Vector3 fallingDeathBackPoint;

    private void Start()
    {
        for (int i = hearts.Count; i < initialLifeNumber; i++)
        {
            AddHeart();
        }
        
        catMovement = transform.GetComponent<CatMovement>();
    }
    private void Update()
    {
        if (hasLost && Input.anyKeyDown)
        {
            SceneManager.LoadScene(interfaceSceneName);
        }
    }

    public void AddHeart()
    {
        if ((hearts.Count == initialLifeNumber && lifeNumber == initialLifeNumber) || hearts.Count < initialLifeNumber)
        {
            Image heart = hearts[hearts.Count - 1];
            Debug.Log("heart: " + heart);
            Vector3 position = heart.transform.position;
            Image newHeart = Instantiate(heart, new Vector3(position.x - 100, position.y, position.z), Quaternion.identity);
            newHeart.transform.SetParent(heart.transform.parent);
            hearts.Add(newHeart);

            lifeNumber = hearts.Count;
        } else
        {
            Image heart = hearts[lifeNumber];
            heart.color = new Color(1, 1, 1, 1f);
            lifeNumber += 1;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "BadGuy")
        {
            LoseOneLife();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallingDeathLine")
        {
            LoseOneLife();
            transform.position = fallingDeathBackPoint;
        }

        if (collision.tag == "FallingDeathBackPoint")
        {
            fallingDeathBackPoint = collision.transform.position;
        }
    }


    private void LoseOneLife ()
    {
        if (!catMovement.isDead)
        {
            Image heart = hearts[lifeNumber - 1];
            heart.color = new Color(0, 0, 0, 0.5f);
            catMovement.Die();
            lifeNumber -= 1;

            if (lifeNumber == 0)
            {
                Lose();
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
                    if(script.GetType().Name != "GameOverTrigger")
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
