using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float RunSpeed;
    private int JumpCount = 0;
    private bool CanJump = true;
    Animator Anim;
    public bool isGameOver = false;
    public GameObject GameoverPanel,scoreText;
    public Text FinalScore, HightScore;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        StartCoroutine("IncreaseGameSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            transform.position = Vector3.right * RunSpeed * Time.deltaTime + transform.position;
        }

        if (JumpCount == 2)
        {
            CanJump = false;
        }
        if (Input.GetKeyDown("space") && CanJump && !isGameOver)
        {
            rb2d.velocity = Vector3.up * 7.5f;
            Anim.SetTrigger("Jump");
            JumpCount += 1;
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Anim.SetTrigger("Death");
        StopCoroutine("IncreaseGameSpeed");

        StartCoroutine("ShowGameoverPanel");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            JumpCount = 0;
            CanJump = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
        if (collision.gameObject.CompareTag("BottomDetector"))
        {
            GameOver();
        }
    }

    IEnumerator IncreaseGameSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            if(RunSpeed < 8)
            {
                RunSpeed += 0.2f;
            }

            if(GameObject.Find("GroundSpawner").GetComponent<ObstacleSpawner>().obstacleSpawnInterval > 1)
            {
                GameObject.Find("GroundSpawner").GetComponent<ObstacleSpawner>().obstacleSpawnInterval -= 0.1f;
            }
        }
    }

    IEnumerator ShowGameoverPanel()
    {
        yield return new WaitForSeconds(2);
        GameoverPanel.SetActive(true);
        scoreText.SetActive(false);

        FinalScore.text = "Score : " + GameObject.Find("ScoreDetector").GetComponent<ScoreSystem>().score;
        HightScore.text = "High Score : " + PlayerPrefs.GetInt("HighScore");
    }

    public void JumpButton()
    {
        if (CanJump && !isGameOver)
        {
            rb2d.velocity = Vector3.up * 7.5f;
            Anim.SetTrigger("Jump");
            JumpCount += 1;
        }
    }
}
