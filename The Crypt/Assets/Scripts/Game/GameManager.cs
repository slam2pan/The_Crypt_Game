using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public bool gameActive;
    public bool levelOver;

    private static GameObject levelInstance;
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI scoreText;
    private PlayerController player;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (levelInstance == null)
        {
            levelInstance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        gameActive = true;
    }
    
    void Update()
    {
        if (gameActive)
        {
            levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.SetText("Level: " + level);

            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            scoreText.SetText("Score: " + Score.GetScore());

            if (player == null)
            {
                player = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
            }        
            
            if (player.Health == 0)
            {
                gameActive = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public bool IsGameActive() {
        return gameActive;
    }

    public void SetGameActive(bool active)
    {
        gameActive = active;
        if (!active)
        {
            level = 1;
        }
    }
}
