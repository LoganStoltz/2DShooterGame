using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController manager;
    
    public GameObject GameOverScreen;

    public TextMeshProUGUI pointsText;

    public int score;

    private void Awake()
    {
        manager = this;
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        pointsText.text = score.ToString() + " POINTS"; 
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
