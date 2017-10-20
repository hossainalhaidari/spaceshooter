using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GUIText scoreText;
    public GUIText gameOverText;
    public GUIText restartText;
    public int hazardCount;
    public float startWait;
    public float spawnWait;
    public float waveWait;
    public bool gameOver;
    public bool restart;

    private int score;

    private void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
        ResetScore();

        LevelController levelController = gameObject.AddComponent<LevelController>();
        levelController.LoadNextLevel();
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over!";
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    private void ResetScore()
    {
        score = 0;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
