using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class TriangleEnemy : Enemies
{
    public LayerMask playerLayer;
    private bool inRange = false;

    public TriangleEnemy()
    {
        this.minSpeed = 2f;
        this.maxSpeed = 5f;
        this.pointValue = 10;
        this.maxHealth = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player(Clone)");
        speed = Random.Range(minSpeed, maxSpeed);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        currentHealth = maxHealth;
        enemyHealthBar = this.GetComponentInChildren<EnemyHealthBar>();
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


}
