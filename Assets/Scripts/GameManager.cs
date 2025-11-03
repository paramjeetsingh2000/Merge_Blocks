using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject winPanel;
    public TextMeshProUGUI scoreText;
    private int score = 0;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        score = 0;
        scoreText.text = "" + score;
    }


    public void WinGame(int number)
    {
        if (number == 2048
            )
        {
            winPanel.SetActive(true);
           
            Time.timeScale = 0f; // Pause game
        }
    }

   public void RestartGame()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
    public void AddScore(int value)
    {
        score += value;
        if (scoreText != null)
            scoreText.text = "" + score;
    }


}


