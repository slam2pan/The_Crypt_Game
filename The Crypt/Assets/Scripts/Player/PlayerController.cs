using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5f;
    private float maxHealth = 10;
    private float playerHealth;
    public float Health
    {
        get { return playerHealth; }
        private set { playerHealth = value; }
    }
    private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerHealth = maxHealth;
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 tempVect = new Vector2(horizontalInput, verticalInput);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        playerRb.MovePosition(playerRb.position + tempVect);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ChangeHealth(-1);
        }
    }

    void ChangeHealth(int amount)
    {
        playerHealth = Mathf.Clamp(playerHealth + amount, 0, maxHealth);
        HealthBar.instance.SetValue(playerHealth / (float)maxHealth);
    }

    // Turn the direction of the character around for ability purposes
    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

}
