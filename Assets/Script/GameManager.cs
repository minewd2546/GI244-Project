using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance of the GameManager
    public bool isGameStarted;
    public GameObject platformSpawner;
    
    [Header("GameStartMenu")]
    public GameObject StartMenuPanel;
    
    [Header("GameOver")]
    public GameObject gameOverPanel;
    public GameObject newHighScoreImage;
    public Text lastScoreText;

    [Header("Score")] 
    public Text scoreText, bestText, coinText, gemText;
    int score = 0;
    private int bestScore, totalCoin, totalGem;
    private bool countScore;
    private bool isPlayerMoving; // Track whether the player is moving

    [Header("Audio")] 
    public AudioSource audioSource;  // เชื่อมโยงกับ AudioSource
    public Button soundButton;       // เชื่อมโยงกับปุ่มที่ใช้เปิด/ปิดเสียง
    private bool isMuted = false;
    
    [Header("UI Shop")]
    public GameObject shopPanel; // Shop Panel UI
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        soundButton.onClick.AddListener(ToggleSound);  // เมื่อกดปุ่มให้เรียกฟังก์ชัน ToggleSound
        //coin
        totalCoin = PlayerPrefs.GetInt("totalCoin");
        coinText.text = totalCoin.ToString();

        //gem
        totalGem = PlayerPrefs.GetInt("totalGem");
        gemText.text = totalGem.ToString();

        //score
        bestScore = PlayerPrefs.GetInt("bestScore");
        bestText.text = bestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted)
        {
            if (Input.GetMouseButtonDown(0)) // Start the game when mouse is clicked
            {
                GameStart();
            }
        }

        // Check if the player is moving (e.g., pressing keys)
        if (isGameStarted)
        {
            isPlayerMoving = CheckIfPlayerIsMoving(); // Detect if the player is moving
        }
    }

    
    // Open the Shop UI
    public void OpenShop()
    {
        shopPanel.SetActive(true); // Show the Shop panel
        StartMenuPanel.SetActive(false); // Hide the Main Menu
    }

    // Close the Shop UI
    public void CloseShop()
    {
        shopPanel.SetActive(false); // Hide the Shop panel
        StartMenuPanel.SetActive(true); // Show the Main Menu
    }
    void ToggleSound()
    {
        isMuted = !isMuted;  // สลับสถานะการเปิด/ปิดเสียง
        audioSource.mute = isMuted;  // ตั้งค่า mute ของ AudioSource
    }
    public void GameStartMenu()
    {
        StartMenuPanel.SetActive(false);
    }
    public void GameStart()
    {
        isGameStarted = true;
        countScore = true;
        StartCoroutine(UpdateScore());
        platformSpawner.SetActive(true);  // Enable platform spawner when the game starts
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        lastScoreText.text = score.ToString();
        countScore = false;
        platformSpawner.SetActive(false); // Disable platform spawner when the game is over

        // Update best score if necessary
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score); // Save the new best score
            newHighScoreImage.SetActive(true);
        }
    }

    // Coroutine to update the score over time
    IEnumerator UpdateScore()
    {
        while (countScore)
        {
            if (isPlayerMoving) // Only update the score if the player is moving
            {
                yield return new WaitForSeconds(1f);
                score++;
                scoreText.text = score.ToString();

                // Update best score if the current score exceeds the best score
                if (score > bestScore)
                {
                    bestText.text = score.ToString();
                }
            }
            else
            {
                yield return null; // Wait until the player starts moving
            }
        }
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("Maingame");
    }
    

    public void GetCoin()
    {
        int newCoin = totalCoin++;
        PlayerPrefs.SetInt("totalCoin", newCoin);
        coinText.text = newCoin.ToString();
    }
    public void GetGem()
    {
        int newGem = totalGem++;
        PlayerPrefs.SetInt("totalGem", newGem);
        gemText.text = newGem.ToString();
    }
    // Check if the player is moving (e.g., pressing keys or input from player)
    bool CheckIfPlayerIsMoving()
    {
        // Example: Check if the player is pressing any key to move
        // You can adjust this based on the actual game controls (WASD, arrow keys, etc.)
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }
}
