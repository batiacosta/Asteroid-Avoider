
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Button continueButton;
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

    public void Continue()
    {
        AdManager.Instance.ShowAd(this);
        continueButton.interactable = false;
    }
    public void ContinueGame()
    {
        scoreSystem.StartTimer();

        asteroidSpawner.enabled = true;
        
        gameOverDisplay.gameObject.SetActive(false);
        
        player.transform.position = Vector3.zero;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.gameObject.SetActive(true);
    }
}
