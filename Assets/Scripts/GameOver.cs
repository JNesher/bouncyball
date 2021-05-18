using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        gameManager.gameRunning = false;
        gameManager.showGameOver();
        gameManager.stopIncreasingDifficulty();
        other.material.bounciness = 0.5f;
        Destroy(other.gameObject);
    }

    public void RestartGame()
    {
        gameManager.StartGame();
    }
}
