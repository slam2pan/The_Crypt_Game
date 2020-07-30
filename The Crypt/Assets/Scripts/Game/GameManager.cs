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

    private int rooms = 4;
    public int Rooms
    {
        get { return rooms; }
    }

    public int minEnemies = 2;
    public int maxEnemies = 5;
    public GameObject[] allEnemies;
    public int triangleSpawnChance = 100;
    public int diamondSpawnChance = 0;

    public bool gameActive;
    public bool levelOver;

    public Sprite[] abilityIcons;

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
            // Hard reset
            level = 1;
            minEnemies = 2;
            maxEnemies = 5;
            triangleSpawnChance = 100;
            diamondSpawnChance = 0;
        }
    }

    public void ChangeLevel()
    {
        levelOver = true;
        IncreaseDifficulty();

        Level++;
        Score.AddToScore(Level * 10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void IncreaseDifficulty()
    {
        // Algorithm to create an S curve graph for number of levels
        rooms = (int) Mathf.Round((12/ (Mathf.Pow(1 + Mathf.Exp(-0.5f * (Level - 22)), 0.1f))));

        // Increase max enemies every other level, and min enemies every 3rd level
        if (Level % 2 == 1)
        {
            maxEnemies++;
        } else if (Level % 3 == 0)
        {
            minEnemies++;
        }

        // Set max cap on diamond spawn chance
        if (Level > 3 && diamondSpawnChance < 20)
        {
            diamondSpawnChance += 1;
            triangleSpawnChance = 100 - diamondSpawnChance;
        }
    }
}
