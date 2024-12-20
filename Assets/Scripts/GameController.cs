using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController manager;
    public GameObject GameOverScreen;
    public TextMeshProUGUI pointsText, inGameScore, inGameWaveNumber;
    public WaveSpawner waveSpawner;  // Add reference to WaveSpawner
    public int score;
    public bool playerDead = false;

    public void Update()
    {
        inGameScore.text = score.ToString() + " POINTS";
        inGameWaveNumber.text = "WAVE: " + waveSpawner.waveCount.ToString();  // Use waveSpawner.waveCount
    }

    private void Awake()
    {
        manager = this;
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        pointsText.text = score.ToString() + " POINTS"; 
        playerDead = true;
    }

    public bool GetPlayerDeathState()
    {
        return playerDead;
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaseScore(int increaseAmount)
    {
        score += increaseAmount;
    }
}
