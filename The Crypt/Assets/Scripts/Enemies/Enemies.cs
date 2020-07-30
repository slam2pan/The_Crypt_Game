using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public abstract class Enemies : MonoBehaviour
{
    [SerializeField] protected float minSpeed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected int pointValue;
    [SerializeField] protected GameObject enemyExplosion;
    protected int maxHealth;

    protected int currentHealth;
    protected float speed;
    protected Rigidbody2D enemyRb;
    protected GameObject player;
    protected GameManager gameManager;
    protected AudioManager audioManager;
    protected EnemyHealthBar enemyHealthBar;
    private bool isQuitting = false;
    private GameObject newAbilityDrop;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("DefaultShot(Clone)"))
        {
            currentHealth -= 1;
            float value = (float) currentHealth / maxHealth;

            enemyHealthBar.SetValue(value);

            if (currentHealth == 0)
            {
                DestroyEnemy(other);
            } else {
                // Still destroy the shot if it doesn't kill
                Destroy(other.gameObject);
            }
        }
    }

    // To avoid double count on the score
    void OnDestroy()
    {
        if (gameManager.levelOver == false && gameManager.IsGameActive() && !isQuitting)
        {
            DropAbility();

            Score.AddToScore(pointValue);
            audioManager.Play("TriangleExplosion");
            GameObject explosionEffect = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            ScorePopup.Create(transform.position, pointValue);
            Destroy(explosionEffect, 0.5f); 
        }
    }

    void DestroyEnemy(Collider2D collision)
    {
        CameraShaker.Instance.ShakeOnce(3f, 2f, .1f, 1f);
        Destroy(gameObject);
        Destroy(collision.gameObject);
    } 

    // Remove error for destroying objects when closing scene
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    
    // Pick a random ability and drop the icon
    void DropAbility()
    {
        int arrIndex = Random.Range(0, gameManager.abilityIcons.Length);

        Instantiate(newAbilityDrop = GameAssets.i.dropIcon, transform.position, Quaternion.identity);
        SpriteRenderer newAbilityDropIcon = newAbilityDrop.transform.Find("DropAbilityIcon").GetComponent<SpriteRenderer>();
        newAbilityDropIcon.sprite = gameManager.abilityIcons[arrIndex];
    }
}
