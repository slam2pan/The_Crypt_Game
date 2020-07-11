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
    public GameObject enemyExplosion;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player(Clone)");
        speed = Random.Range(minSpeed, maxSpeed);
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
            GameObject explosionEffect = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            Destroy(explosionEffect, 0.5f);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
