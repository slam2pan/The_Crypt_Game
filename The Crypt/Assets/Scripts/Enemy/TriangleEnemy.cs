using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleEnemy : MonoBehaviour
{
    public LayerMask playerLayer;
    private bool inRange = false;
    private float speed;
    private float minSpeed = 2f;
    private float maxSpeed = 5f;
    private Rigidbody2D enemyRb;
    private GameObject player;
    private GameManager gameManager;
    public GameObject enemyExplosion;
    private AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player(Clone)");
        speed = Random.Range(minSpeed, maxSpeed);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        // Continuously follow the player after the player enters its range
        if (inRange)
        {
            enemyRb.MovePosition(enemyRb.position + (Vector2)((player.transform.position - transform.position).normalized * speed * Time.deltaTime));
        }
        else
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 4f, playerLayer);
            if (collider != null)
            {
                inRange = true;
            }
        }
    }

    // Triangle Enemy dies in one shot, bullet disappears as well
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("DefaultShot(Clone)"))
        {
            audioManager.Play("TriangleExplosion");
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameObject explosionEffect = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            ScorePopup.Create(transform.position, 10);
            Destroy(explosionEffect, 0.5f); 
        }
    }

    // To avoid double count on the score
    void OnDestroy()
    {
        if (gameManager.levelOver == false && gameManager.IsGameActive())
        {
            Score.AddToScore(10);
        }
    }
}
