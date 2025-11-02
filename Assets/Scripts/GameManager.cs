using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject winPanel;
   

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

    public void WinGame(int number)
    {
        if (number == 2048
            )
        {
            winPanel.SetActive(true);
           
            Time.timeScale = 0f; // Pause game
        }
    }
}


