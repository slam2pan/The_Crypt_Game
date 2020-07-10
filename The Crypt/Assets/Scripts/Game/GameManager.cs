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
    private bool gameActive;

    private static GameObject levelInstance;
    private TextMeshProUGUI levelText;
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

    void Update()
    {
        if (gameActive)
        {
            levelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();
            levelText.SetText("Level: " + level);

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
}
