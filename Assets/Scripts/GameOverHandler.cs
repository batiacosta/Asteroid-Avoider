using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    
    public void EndGame()
    {
        int score = scoreSystem.GetScore();
        gameOverText.text = $"Game over\nScore: {score}";
        asteroidSpawner.enabled = false;
        gameOverDisplay.gameObject.SetActive(true);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
