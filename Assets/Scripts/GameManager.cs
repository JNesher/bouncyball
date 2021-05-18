using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI uhOhText;
    public Button restartButton;
    public Button startButton;
    [SerializeField] SpawnManager spawnManager;
    public Ball ball;
    public bool gameRunning = false;
    private int score = 0;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject startingPlat;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    public void StartGame()
    {
        titleText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + 0;
        ball = Instantiate(ballPrefab, new Vector3(0,20,0), new Quaternion()).GetComponent<Ball>();
        Instantiate(startingPlat);
        gameRunning = true;
        InvokeRepeating("IncreaseDifficulty", 10f, 10f);
        spawnManager.ResetDefaults();
        spawnManager.timeLeft = 0;
    }

    public void stopIncreasingDifficulty()
    {
        CancelInvoke();
    }

    public void UpdateScore()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    public void showGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    void IncreaseDifficulty()
    {
        int choice = Random.Range(0, 3);
        StartCoroutine(BlinkUhOh(choice));
        switch (choice)
        {
            case 0:
                spawnManager.SpeedUpPlatform();
                break;
            case 1:
                spawnManager.makePlatformSmaller();
                break;
            case 2:
                spawnManager.spacePlatformsOut();
                break;
            case 3:
                ball.lowerHeight();
                break;
            default:
                Debug.Log("Didn't choose a method");
                break;
        }
    }

    IEnumerator BlinkUhOh(int choice)
    {
        string[] choices = { "Faster!", "Smaller!", "Wider!", "Lower!" };
        uhOhText.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        uhOhText.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        uhOhText.text = choices[choice];
        uhOhText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        uhOhText.gameObject.SetActive(false);
        uhOhText.text = "UH OH!";

    }
}
