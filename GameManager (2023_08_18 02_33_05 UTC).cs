using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float resetDelay = 2f;
    public int damageAmount = 20;

    private bool isGameOver = false;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over");
            // Add any additional game over logic here

            // Call the RestartGame function after a delay
            Invoke("RestartGame", resetDelay);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
