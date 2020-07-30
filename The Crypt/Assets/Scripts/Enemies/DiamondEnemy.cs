using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondEnemy : Enemies
{
    private float coolDownTimer = 3;
    private float shotVelocity = 3;
    private float cooldown;

    public DiamondEnemy()
    {
        this.minSpeed = 2f;
        this.maxSpeed = 5f;
        this.pointValue = 25;
        this.maxHealth = 3;
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

        cooldown = coolDownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        Vector2 direction = new Vector2(0,0);

        if (cooldown < 0)
        {
            // Dash ability with a cooldown
            direction = (player.transform.position - transform.position).normalized;
            enemyRb.velocity = direction * shotVelocity;

            cooldown = coolDownTimer;
        }
    }
}
